using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace WechatAppUnitTest
{
    public class AppConfigurtaionServices
    {
        public static T GetAppSettings<T>(string key) where T : class, new()
        {
            IConfiguration config = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "NoSql.json", ReloadOnChange = true })
            .Build();
            var appconfig = new ServiceCollection()
            .AddOptions()
            .Configure<T>(config.GetSection(key))
            .BuildServiceProvider()
            .GetService<IOptions<T>>()
            .Value;
            return appconfig;
        }
    }
}
