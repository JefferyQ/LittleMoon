using System;

namespace MongoDbCommon
{
    /// <summary>
    /// Mongodb持久化PO父类，提供增删改时间功能
    /// </summary>
    public class MongoDbBasePo
    {
        private DateTime _utcCreateTime;

        /// <summary>
        /// 创建时间(UTC)
        /// </summary>
        public DateTime UtcCreateTime
        {
            get => _utcCreateTime == default(DateTime) ? (_utcCreateTime = DateTime.UtcNow) : _utcCreateTime;
            set => _utcCreateTime = value;
        }

        private DateTime _utcModifyTime;

        /// <summary>
        /// 修改时间(UTC)
        /// </summary>
        public DateTime UtcModifyTime
        {
            get => _utcModifyTime == default(DateTime) ? (_utcModifyTime = DateTime.UtcNow) : _utcModifyTime;
            set => _utcModifyTime = value;
        }

        private DateTime _utcCancelTime;
        /// <summary>
        /// 作废时间(UTC)
        /// </summary>
        public DateTime? UtcCancelTime
        {
            get => _utcCancelTime;
            set => _utcCancelTime = value ?? new DateTime(2001, 1, 1).ToUniversalTime();
        }
    }
}