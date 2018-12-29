using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace CommUtils.Helper
{
    public class PostRequestHelper
    {
        private static HttpWebRequest GetWebRequest(string url, string method)
        {
            var req = (HttpWebRequest) WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            req.KeepAlive = true;
            req.UserAgent = "Top4Net";
            req.Timeout = 100000;
            return req;
        }

        private static string GetResponse(HttpWebResponse rsp, Encoding encoding)
        {
            // 以字符流的方式读取HTTP响应
            using (var stream = rsp.GetResponseStream())
            {
                if (stream == null) throw new WebException("请求失败！");
                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static string PostData(string url, string strParam)
        {
            var req = GetWebRequest(url, "POST");
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            var gParams = Encoding.UTF8.GetBytes(strParam);
            var reqStream = req.GetRequestStream();
            reqStream.Write(gParams, 0, gParams.Length);
            reqStream.Close();

            using (var rsp = (HttpWebResponse) req.GetResponse())
            {
                if (rsp.CharacterSet == null)
                    return string.Empty;

                var encoding = Encoding.GetEncoding("UTF-8");
                return GetResponse(rsp, encoding);
            }
        }
        private static string PostData(string url, IDictionary<string, string> paramDic)
        {
            var req = GetWebRequest(url, "POST");
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            var gParams = Encoding.UTF8.GetBytes(GetParams(paramDic));
            var reqStream = req.GetRequestStream();
            reqStream.Write(gParams, 0, gParams.Length);
            reqStream.Close();

            using (var rsp = (HttpWebResponse)req.GetResponse())
            {
                if (rsp.CharacterSet == null)
                    return string.Empty;

                var encoding = Encoding.GetEncoding("UTF-8");
                return GetResponse(rsp, encoding);
            }
        }
        public static string GetParams(IDictionary<string, string> paramDic)
        {
            StringBuilder paramsBuilder = new StringBuilder(30);

            foreach (var para in paramDic)
            {
                paramsBuilder.AppendFormat("{0}={1}&", para.Key, HttpUtility.UrlEncode(para.Value));
            }

            var paramsStr = paramsBuilder.ToString();
            var lastIndex = paramsStr.LastIndexOf("&");
            if (!string.IsNullOrEmpty(paramsStr) && lastIndex > 0)
            {
                paramsStr = paramsStr.Substring(0, paramsStr.LastIndexOf("&"));
            }

            return paramsStr;
        }

        public static string Post(string url, string strParam)
        {
            //请求失败重试4次
            while (true)
            {
                try
                {
                    return PostData(url, strParam);
                }
                catch (WebException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        /// <summary>
        /// 描述：发送请求
        /// 作者：陈珙
        /// 日期：2015-05-20
        /// 版本：V1.0
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="paramDic">请求参数</param>
        /// <returns></returns>
        public static string Post(string url, IDictionary<string, string> paramDic)
        {
            //请求失败重试4次
            while (true)
            {
                return PostData(url, paramDic);
            }
        }
    }
}
