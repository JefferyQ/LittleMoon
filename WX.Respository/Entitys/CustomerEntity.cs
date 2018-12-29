using MongoDbCommon;
using System.ComponentModel.DataAnnotations.Schema;

namespace WX.Respository
{
    [Table("Customer")]
    public class CustomerEntity : MongoDbBasePo
    {
        public string Id { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别:1男，2女
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 是否已注册会员
        /// </summary>
        public bool IsVIP { get;set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string ImgUrl { get; set; }
    }
}
