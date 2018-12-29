using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WX.Respository;
using System.Linq;

namespace WechatAppUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var config = AppConfigurtaionServices.GetAppSettings<NoSqlCoreService.MongoDbConfiger>("MongodbConnStr");
            var customer = new CustomerEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name="��Ѷ66",
                NickName = "��������",
                Age = 25,
                Gender = 1,
                IsVIP = true,
            };
            var mongodbService = new NoSqlCoreService.MongoDbService(config);
            mongodbService.Add(customer);
            var data = mongodbService.GetQueryable<CustomerEntity>().Where(p => p.Name == "��Ѷ" && p.Age == 25).FirstOrDefault();
        }
    }
    public class MongodbConn
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
