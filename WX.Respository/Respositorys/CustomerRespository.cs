using NoSqlCoreService;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WX.Respository
{
    public class CustomerRespository
    {
        private static MongoDbService mongoDbService;
        static CustomerRespository()
        {
            mongoDbService = mongoDbService ?? new MongoDbService();
        }
        public void AddCustomer(CustomerEntity customer)
        {
            mongoDbService.Add(customer);
        }
        public IQueryable<CustomerEntity> GetCustomerQuery()
        {
            return mongoDbService.GetQueryable<CustomerEntity>();
        }
        public CustomerEntity GetCustomer(string id)
        {
            return mongoDbService.GetQueryable<CustomerEntity>().FirstOrDefault(a => a.Id == id);
        }
        public void UpdateCustomer(CustomerEntity customer)
        {
            mongoDbService.Update(customer);
        }
        public void UpdateCustomer(Expression<Func<CustomerEntity>> update, Expression<Func<CustomerEntity, bool>> where)
        {
            mongoDbService.Update(update, where);
        }
        public void DeleteCustomer(Expression<Func<CustomerEntity, bool>> where)
        {
            mongoDbService.Delete(where);
        }
    }
}
