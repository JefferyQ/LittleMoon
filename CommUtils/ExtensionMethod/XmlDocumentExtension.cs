using System.Xml;

public static class XmlDocumentExtension
{
    /// <summary>
    /// 获取xml里面单个不重复节点的值
    /// </summary>
    /// <param name="doc">xml文档</param>
    /// <param name="tag">tag节点</param>
    /// <returns>如果存在，并且只有一个则返回。否则返回null。</returns>
    public static string GetSingleTagValue(this XmlDocument doc, string tag)
    {
        var node = doc.GetElementsByTagName(tag);
        return node.Count == 0 ? null : node[0].InnerText;
        //todo
    }


    /// <summary>
    /// 获取xml里面第一个节点的值
    /// </summary>
    /// <param name="doc">xml文档</param>
    /// <param name="tag">tag节点</param>
    /// <returns>如果存在，则返回第一个的值。否则返回null。</returns>
    public static string GetFirstTagValue(this XmlDocument doc, string tag)
    {
        var node = doc.GetElementsByTagName(tag);
        return node.Count == 0 ? null : node[0].InnerText;
    }
}