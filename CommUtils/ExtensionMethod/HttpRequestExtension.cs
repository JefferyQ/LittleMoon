using System.IO;
using System.Net;

public static class HttpRequestExtension
{
    /// <summary>
    /// req的简便操作 发送请求，获取返回内容字符串
    /// </summary>
    /// <param name="req"></param>
    /// <param name="value">要post的内容</param>
    public static string GetResponseContent(this HttpWebRequest req, string value=null)
    {
        if(value!=null) req.WriteContent(value);
        return req.GetFinalResponse().GetResponseContent();
    }

    /// <summary>
    /// Response的简便操作 , 获取返回内容字符串
    /// </summary>
    public static string GetResponseContent(this HttpWebResponse resp)
    {
        var responseStream = resp.GetResponseStream();
        var streamReader = new StreamReader(responseStream);
        string result = streamReader.ReadToEnd();
        resp.Close();
        responseStream.Close();
        streamReader.Close();
        return result;
    }

    /// <summary>
    /// req的简便操作 获取response
    /// </summary>
    public static HttpWebResponse GetFinalResponse(this HttpWebRequest req)
    {
        WebResponse resp;
        try
        {
            resp = req.GetResponse();
        }
        catch (WebException ex)
        {
            resp = ex.Response;
        }
        return resp as HttpWebResponse;
    }

    /// <summary>
    ///  req的简便操作 发送内容
    /// </summary>
    /// <param name="req"></param>
    /// <param name="content">要post的内容</param>
    public static void WriteContent(this HttpWebRequest req, string content)
    {
        StreamWriter writer = new StreamWriter(req.GetRequestStream());
        writer.Write(content);
        writer.Close();
    }
}
