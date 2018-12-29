using System;
using System.Collections.Generic;
using System.Linq;

namespace NoSqlCoreService
{
    /// <summary>
    /// mongodb配置类
    /// </summary>
    public class MongoDbConfiger
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host"></param>
        /// <param name="database"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="readPreference"></param>
        public MongoDbConfiger(string host, string database, string userName, string password,
            MongoDbReadPreference readPreference)
        {
            Host = host;
            Database = database;
            UserName = userName;
            Password = password;
            ReadPreference = readPreference;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hosts"></param>
        /// <param name="database"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="readPreference"></param>
        public MongoDbConfiger(List<string> hosts, string database, string userName, string password,
            MongoDbReadPreference readPreference)
        {
            Hosts = hosts;
            Database = database;
            UserName = userName;
            Password = password;
            ReadPreference = readPreference;
        }
        public MongoDbConfiger() { }
        /// <summary>
        /// 服务器
        /// </summary>
        public string Host { get; set; }

        private List<string> _hosts;

        /// <summary>
        /// 服务器集群
        /// </summary>
        private List<string> Hosts
        {
            get => _hosts ?? (_hosts = new List<string>());
            set => _hosts = value;
        }

        /// <summary>
        /// 数据库
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        private string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        private string Password { get; set; }

        /// <summary>
        /// 读倾向
        /// </summary>
        private MongoDbReadPreference ReadPreference { get; }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            return Hosts.Any()
                ? $"mongodb://{UserName}:{Password}@{string.Join(",", Hosts)}/{Database}/?readPreference={GetReadPreferenceStr()}"
                : $"mongodb://{Host}/{Database}/?readPreference={GetReadPreferenceStr()}";
        }

        /// <summary>
        /// 获取读倾向对应的字符串
        /// </summary>
        /// <returns></returns>
        private string GetReadPreferenceStr()
        {
            switch (ReadPreference)
            {
                case MongoDbReadPreference.Primary:
                    return "primary";
                case MongoDbReadPreference.PrimaryPreferred:
                    return "primaryPreferred";
                case MongoDbReadPreference.Secondary:
                    return "secondary";
                case MongoDbReadPreference.SecondaryPreferred:
                    return "secondaryPreferred";
                case MongoDbReadPreference.Nearest:
                    return "mearest";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}