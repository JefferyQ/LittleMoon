using System.IO;

namespace CommUtils.Helper
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 从文件读取到Stream
        /// </summary>
        /// <param name="fullname">文件完整路径</param>
        /// <returns></returns>
        public Stream FileToMemoryStream(string fullname)
        {
            var bytes = FileToBytes(fullname);
            // 把 byte[] 转换成 Stream   
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// 从文件读取到字节数组
        /// </summary>
        /// <param name="fullname">文件完整路径</param>
        /// <returns></returns>
        public byte[] FileToBytes(string fullname)
        {
            byte[] bytes;
            // 打开文件   
            using (var fileStream = new FileStream(fullname, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // 读取文件的 byte[]   
                bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
            }
            return bytes;
        }

        /// <summary>
        /// 将源目录文件及目录拷到目的文件夹
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destDir"></param>
        /// <param name="overwriteexisting"></param>
        public void CopyDirectory(string sourceDir, string destDir, bool overwriteexisting = true)
        {
            const string directoryChar = @"\";
            sourceDir = sourceDir.EndsWith(directoryChar) ? sourceDir : sourceDir + directoryChar;
            destDir = destDir.EndsWith(directoryChar) ? destDir : destDir + directoryChar;
            if (!Directory.Exists(sourceDir))
                return;
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
            foreach (var fls in Directory.GetFiles(sourceDir))
            {
                var flinfo = new FileInfo(fls);
                flinfo.CopyTo(destDir + flinfo.Name, overwriteexisting);
            }
            foreach (var drs in Directory.GetDirectories(sourceDir))
            {
                var drinfo = new DirectoryInfo(drs);
                CopyDirectory(drs, destDir + drinfo.Name, overwriteexisting);
            }
        }

        /// <summary>
        /// 根据文件夹全路径逐层创建目录
        /// </summary>
        /// <param name="dirPath"></param>
        public void CreateDirectories(string dirPath)
        {
            if (Directory.Exists(dirPath))
                return;
            var dirArray = dirPath.Split('\\');
            var temp = string.Empty;
            for (var i = 0; i < dirArray.Length - 1; i++)
            {
                temp += dirArray[i].Trim() + "\\";
                if (!Directory.Exists(temp))
                    Directory.CreateDirectory(temp);
            }
        }
    }
}