using NoSqlCoreService.MongoDbAccessor;
using CommUtils.ExtensionMethod;
using MongoDbCommon;
using MongoDB.Driver;
using System.Collections.Concurrent;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using CommUtils.Helper;

namespace NoSqlCoreService
{
    public class MongoDbService
    {
        /// <summary>
        /// 连接设置
        /// </summary>
        private MongoCollectionSettings _collectionSettings;

        /// <summary>
        /// MongoDB数据库
        /// </summary>
        private IMongoDatabase MongoDatabase { get; set; }

        /// <summary>
        /// MongoDB数据库
        /// </summary>
        private MongoDatabase MongoDb { get; set; }

        /// <summary>
        /// MongoDB客户端
        /// </summary>
        private MongoClient MongoClient { get; set; }

        /// <summary>
        /// 表名字典
        /// </summary>
        private static readonly ConcurrentDictionary<Type, string> TableNameDic =
            new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// MongoClient字典，实际上是连接池字典
        /// </summary>
        private static readonly ConcurrentDictionary<string, MongoClient> Clients =
            new ConcurrentDictionary<string, MongoClient>();

        private static readonly object LockObj = new object();

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetTableName(Type type)
        {
            return TableNameDic.GetOrAdd(type, key =>
            {
                var attrs = Attribute.GetCustomAttributes(type);
                var tableAttr = attrs.OfType<TableAttribute>().FirstOrDefault();
                var tableName = tableAttr == null ? type.Name : tableAttr.Name;
                return tableName;
            });
        }

        /// <summary>
        /// 取默认集合操作设置。
        /// </summary>
        /// <returns></returns>
        private MongoCollectionSettings CollectionSettings
            => _collectionSettings ?? (_collectionSettings = new MongoCollectionSettings
            {
                AssignIdOnInsert = true,
                GuidRepresentation = GuidRepresentation.CSharpLegacy,
                ReadPreference = ReadPreference.Primary,
                WriteConcern = WriteConcern.WMajority,
                ReadConcern = ReadConcern.Default
            });

        /// <summary>
        /// 构造MongoDbService对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fileName"></param>
        public MongoDbService(string key = "MongodbConnStr", string fileName = "NoSql.json")
        {
            var config = ConfigHelper.GetAppSettings<MongoDbConfiger>("MongodbConnStr",fileName);
            InitMember(config.GetConnectionString(), config.Database);
        }

        /// <summary>
        /// 根据Mongodb配置类构造MongoDbService对象
        /// </summary>
        /// <param name="configer"></param>
        public MongoDbService(MongoDbConfiger configer)
        {
            InitMember(configer.GetConnectionString(), configer.Database);
        }

        private void InitMember(string connStr, string database)
        {
            InitClient(connStr);
            MongoDatabase = MongoClient.GetDatabase(database);
            MongoDb = MongoClient.GetServer().GetDatabase(database);
        }

        private void InitClient(string connStr)
        {
            if (MongoClient != null) return;
            lock (LockObj)
            {
                if (MongoClient != null) return;
                MongoClient = Clients.GetOrAdd(connStr, key => new MongoClient(connStr));
            }
        }

        /// <summary>
        /// 返回查询结果
        /// </summary>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> Find<T>(Expression<Func<T, bool>> filter) where T : class, new()
        {
            return MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings).Find(filter).ToList();
        }

        /// <summary>
        /// 返回单个查询结果
        /// </summary>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FirstOrDefault<T>(Expression<Func<T, bool>> filter) where T : class, new()
        {
            return MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings).Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// 根据条件进行计数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Count<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where));
            var theCollection = MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings);
            return theCollection.Count(where);
        }

        /// <summary>
        /// 返回查询对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetQueryable<T>() where T : class, new()
        {
            return MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings).AsQueryable();
        }

        /// <summary>
        /// 添加一个实体，要求存在实体类型到数据库表的映射
        /// </summary>
        /// <param name="entity"></param>
        public void Add<T>(T entity) where T : class
        {
            MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings).InsertOne(entity);
        }

        /// <summary>
        /// 添加多个同类型的实体
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange<T>(IEnumerable<T> entities) where T : class, new()
        {
            MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings).InsertMany(entities);
        }

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Delete<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            if (where == null)
                throw new ArgumentNullException(nameof(where));

            var deleteWhere = TranslateWhere(where);

            var theCollection = MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings);
            var result = theCollection.DeleteMany(deleteWhere);

            return result.DeletedCount;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Delete<T>(T entity) where T : class, new()
        {
            return MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings)
                .DeleteOne(GetJsonFilterDefintion(entity)).DeletedCount;
        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="update"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public long Update<T>(Expression<Func<T>> update, Expression<Func<T, bool>> where) where T : class, new()
        {
            if (null == update)
                return 0;

            if (where == null)
                throw new ArgumentNullException(nameof(where));

            var updateWhere = TranslateWhere(where);
            var updateSet = TranslateUpdate(update);

            var theCollection = MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings);
            var result = theCollection.UpdateMany(updateWhere, updateSet);

            return result.ModifiedCount;
        }

        /// <summary>
        /// 更新一个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Update<T>(T entity)
        {
            if (null == entity)
                return 0;

            var collection = MongoDatabase.GetCollection<T>(GetTableName(typeof(T)), CollectionSettings);

            var filter = GetJsonFilterDefintion(entity);

            var basePo = entity as MongoDbBasePo;
            if (basePo != null)
                basePo.UtcModifyTime = DateTime.UtcNow;

            var versionPo = entity as MongoDbVersionPo;
            if (versionPo != null)
                versionPo.DataVersion++;

            var result = collection.UpdateOne(filter,
                new BsonDocumentUpdateDefinition<T>(new BsonDocument { { "$set", entity.ToBsonDocument() } }));

            if (result.ModifiedCount == 0 && versionPo != null)
                versionPo.DataVersion--;

            return result.ModifiedCount;
        }

        /// <summary>
        /// 组装Json Filter Defintion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>返回id：value</returns>
        private static JsonFilterDefinition<T> GetJsonFilterDefintion<T>(T entity)
        {
            var propertyInfo = entity.GetType().GetProperty("Id");
            if (propertyInfo == null) throw new NullReferenceException("主键名必须为Id。");

            var value = propertyInfo.GetValue(entity, null);

            string json;
            var isDigit = propertyInfo.PropertyType == Types.Int32 || propertyInfo.PropertyType == Types.Int64 ||
                          propertyInfo.PropertyType == Types.Int16 || propertyInfo.PropertyType == Types.UInt32;
            var versionPo = entity as MongoDbVersionPo;

            if (isDigit)
                if (versionPo != null && versionPo.DataVersion != 0)
                    json = "{\"_id\":" + value + ",\"DataVersion\":" + versionPo.DataVersion + "}";
                else
                    json = "{\"_id\":" + value + "}";
            else if (versionPo != null && versionPo.DataVersion != 0)
                json = "{\"_id\":\"" + value + "\",\"DataVersion\":" + versionPo.DataVersion + "}";
            else
                json = "{\"_id\":\"" + value + "\"}";

            return new JsonFilterDefinition<T>(json);
        }

        /// <summary>
        /// 翻译Where部分的Lambda表达式。得到独立的Where部分，用来拼装带Where的删除和更新。
        /// </summary>
        /// <typeparam name="T">使用条件的类型参数。</typeparam>
        /// <param name="where">Where条件表达式。</param>
        /// <returns>翻译结果。</returns>
        private BsonDocument TranslateWhere<T>(Expression<Func<T, bool>> where) where T : class, new()
        {
            BsonDocument theWhereDocument;
            var collection = MongoDb.GetCollection<T>(GetTableName(typeof(T)));
            try
            {
                var theQurePv = new MongoQueryProvider(collection);
                var queryWhere = collection.AsQueryable().Where(where);
                //var mongoQuery = queryWhere as MongoQueryable<T>;
                var translatedQuery = MongoQueryTranslator.Translate(queryWhere);
                var selectQuery = translatedQuery as SelectQuery;
                theWhereDocument = collection.AsQueryable().Where(where).ToBsonDocument();
            }
            catch (Exception ex)
            {
                throw new Exception("where条件未能正确解析", ex);
            }

            return theWhereDocument;
        }

        /// <summary>
        /// 翻译Update部分的Lambda表达式。得到要更新的属性或字段。
        /// </summary>
        /// <typeparam name="T">使用条件的类型参数。</typeparam>
        /// <param name="update">Update表达式。</param>
        /// <returns>翻译结果。</returns>
        private static BsonDocument TranslateUpdate<T>(Expression<Func<T>> update) where T : new()
        {
            if (update.NodeType != ExpressionType.Lambda) throw new Exception("参数Update不是正确的Lambda表达式");
            var updatebody = update.Body as MemberInitExpression;
            var analysiser = new InitExpresPropAnalysiser();
            var updatedPropList = analysiser.Analysiser(updatebody);
            var mongoUpdate = new MongoDB.Driver.Builders.UpdateBuilder();
            foreach (var membVal in updatedPropList)
                mongoUpdate.SetWrapped(membVal.MemberRoute, membVal.Value);
            return mongoUpdate.ToBsonDocument();
        }
    }
}