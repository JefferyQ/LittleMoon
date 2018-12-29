using System;
using WX.Respository;
using CommUtils.ExtensionMethod;
using CommUtils;

namespace WX.Domain
{
    public class CustomerDomain
    {
        private static readonly CustomerRespository customerRespository;
        static CustomerDomain()
        {
            customerRespository = new CustomerRespository();
        }
        public void AddCustomer(CustomerModel model)
        {
            if (model.Name.IsNullOrWhiteSpace())
                ThrowHelper.CreateArgumentException("客户名称不能为空");
            model.Id = Guid.NewGuid().ToString();
            customerRespository.AddCustomer(model.ToEntity());
        }
        public void UpdateCustomer(CustomerModel model)
        {
            var customer = customerRespository.GetCustomer(model.Id);
            if (customer == null)
                ThrowHelper.CreateArgumentException("找不到该客户！");
            customer.NickName = model.NickName;
            customer.Name = model.Name;
            customer.IsVIP = model.IsVIP;
            customer.Phone = model.Phone;
            customerRespository.UpdateCustomer(customer);
        }
        public CustomerModel GetCustomer(string id)
        {
            var customer = customerRespository.GetCustomer(id);
            return customer.ToModel();
        }
    }
}
