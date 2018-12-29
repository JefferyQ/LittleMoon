namespace MongoDbCommon
{
    /// <summary>
    /// Mongodb数据版本类，提供数据版本控制
    /// </summary>
    public class MongoDbVersionPo:MongoDbBasePo
    {
        /// <summary>
        /// 数据版本
        /// </summary>
        public int DataVersion { get; set; }
    }
}