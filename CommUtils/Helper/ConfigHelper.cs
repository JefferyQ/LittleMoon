using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CommUtils.Helper
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelper
    {
        public static T GetAppSettings<T>(string key,string fileName) where T : class, new()
        {
            IConfiguration config = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = fileName, ReloadOnChange = true })
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