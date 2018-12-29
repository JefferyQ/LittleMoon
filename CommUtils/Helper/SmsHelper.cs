using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommUtils.Helper
{
    /// <summary>
    /// 有易短信推送服务API辅助类
    /// http://sms.ue35.net
    /// </summary>
    public class SmsHelper
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        private const string Username = "ftwl";

        /// <summary>
        /// 用户密码
        /// </summary>
        private const string Userpwd = "171221";

        /// <summary>
        /// 将c# Unix时间戳转换为DateTime时间
        /// </summary>
        /// <param name="unixTimestamp"></param>
        /// <returns></returns>
        public static DateTime ConvertIntDateTime(double unixTimestamp)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var time = startTime.AddSeconds(unixTimestamp);
            return time;
        }

        /// <summary> 
        /// 将c# DateTime时间转换为Unix时间戳格式
        /// </summary> 
        /// <param name="time"> 时间 </param> 
        /// <returns> double </returns> 
        public static double ConvertDateTimeInt(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            var intResult = (time - startTime).TotalSeconds;
            return intResult;
        }

        /// <summary>
        /// 发送短信接口
        /// http://sms.ue35.net/sms/interface/sendmess.htm
        /// </summary>
        /// <param name="mobiles">手机号码列表，最大1000个，号码间以英文分号 ; 分隔</param>
        /// <param name="content">要提交的短信内容，中文内容要使用UTF-8字符集进行URL编码，避免有特殊符号造成提交失败</param>
        /// <param name="sendtime">发送时间，用于定时短信。这里使用的是Unix时间戳。转换函数 ConvertDateTimeInt、ConvertIntDateTime</param>
        /// <returns></returns>
        public static async Task<string> SendMess(string mobiles, string content, string sendtime = "")
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(mobiles), "mobiles is null or whitespace.");
            Debug.Assert(!string.IsNullOrWhiteSpace(content), "content is null or whitespace.");

            if (string.IsNullOrWhiteSpace(mobiles) || string.IsNullOrWhiteSpace(content))
            {
                throw new Exception("mobiles or content is null or whitespace.");
            }

            const string url = "http://sms.ue35.net/sms/interface/sendmess.htm";

            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };

            using (var http = new HttpClient(handler))
            {
                var mobilecount = mobiles.Split(';').Count().ToString(CultureInfo.InvariantCulture);
                var dict = new Dictionary<string, string>
                {
                    {"username", Username},
                    {"userpwd", Userpwd},
                    {"mobiles", mobiles},
                    {"content", content},
                    {"mobilecount", mobilecount},
                };
                if (!string.IsNullOrWhiteSpace(sendtime))
                {
                    dict["sendtime"] = sendtime;
                }
                var encodedContent = new FormUrlEncodedContent(dict);
                var response = await http.PostAsync(url, encodedContent);

                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// 查询余额接口
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetBalance()
        {
            const string url = "http://sms.ue35.net/sms/interface/getbalance.htm";

            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };

            using (var http = new HttpClient(handler))
            {
                var dict = new Dictionary<string, string>
                {
                    {"username", Username},
                    {"userpwd", Userpwd},
                };
                var encodedContent = new FormUrlEncodedContent(dict);
                var response = await http.PostAsync(url, encodedContent);

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
