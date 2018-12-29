using CommUtils;
using System.Collections.Generic;
using System.Linq;
using WX.Respository;

namespace WX.Domain
{
    public class CustomerModel
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
        public bool IsVIP { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string ImgUrl { get; set; }
    }
    public static class CustomerModelConver
    {
        public static CustomerModel ToModel(this CustomerEntity customerEntity)
        {
            return ConvertHelper.ConvertTo<CustomerEntity, CustomerModel>(customerEntity);
        }
        public static List<CustomerModel> ToModels(this List<CustomerEntity> customerEntities)
        {
            return customerEntities.Select(ToModel).ToList();
        }
        public static CustomerEntity ToEntity(this CustomerModel customerModel)
        {
            return ConvertHelper.ConvertTo<CustomerModel, CustomerEntity>(customerModel);
        }
        public static List<CustomerEntity> ToEntitys(this List<CustomerModel> customerModels)
        {
            return customerModels.Select(ToEntity).ToList();
        }
    }
}
