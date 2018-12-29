using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CommUtils.Helper
{
    public class SecurityHelper
    {
        /// <summary>
        /// DES加密
        /// 作者：杜小非
        /// 日期：2015-04-29
        /// 版本：V1.0
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptString(string encryptString, string key = "idealhere")
        {
            if (string.IsNullOrEmpty(encryptString))
                throw new ArgumentNullException(nameof(encryptString), @"不能为空");

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var keyIv = keyBytes;
            var inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            var resultByteArray = EncryptBytes(inputByteArray, keyBytes, keyIv);

            return Convert.ToBase64String(resultByteArray);
        }

        /// <summary>
        /// DES加密
        /// 作者：杜小非
        /// 日期：2015-04-29
        /// 版本：V1.0
        /// </summary>
        /// <param name="sourceBytes"></param>
        /// <param name="keyBytes"></param>
        /// <param name="keyIv"></param>
        /// <returns></returns>
        private static byte[] EncryptBytes(byte[] sourceBytes, byte[] keyBytes, byte[] keyIv)
        {
            if (sourceBytes == null)
                throw new ArgumentNullException(nameof(sourceBytes), @"不能为空。");
            if (keyBytes == null)
                throw new ArgumentNullException(nameof(keyBytes), @"不能为空。");
            if (keyIv == null)
                throw new ArgumentNullException(nameof(keyIv), @"不能为空。");

            keyBytes = CheckByteArrayLength(keyBytes);
            keyIv = CheckByteArrayLength(keyIv);

            var provider = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIv),
                CryptoStreamMode.Write);

            cStream.Write(sourceBytes, 0, sourceBytes.Length);
            cStream.FlushFinalBlock();

            var buffer = mStream.ToArray();

            mStream.Close();
            cStream.Close();

            return buffer;
        }

        /// <summary>
        /// DES解密
        /// 作者：杜小非
        /// 日期：2015-04-29
        /// 版本：V1.0
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptString(string decryptString, string key = "idealhere")
        {
            if (string.IsNullOrEmpty(decryptString))
                throw new ArgumentNullException(nameof(decryptString), @"不能为空");
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var keyIv = keyBytes;
            var inputByteArray = Convert.FromBase64String(decryptString);
            var resultByteArray = DecryptBytes(inputByteArray, keyBytes, keyIv);

            return Encoding.UTF8.GetString(resultByteArray);
        }

        /// <summary>
        /// DES解密
        /// 作者：杜小非
        /// 日期：2015-04-29
        /// 版本：V1.0
        /// </summary>
        /// <param name="sourceBytes"></param>
        /// <param name="keyBytes"></param>
        /// <param name="keyIv"></param>
        /// <returns></returns>
        private static byte[] DecryptBytes(byte[] sourceBytes, byte[] keyBytes, byte[] keyIv)
        {
            if (sourceBytes == null)
                throw new ArgumentNullException(nameof(sourceBytes), @"不能为空。");
            if (keyBytes == null)
                throw new ArgumentNullException(nameof(keyBytes), @"不能为空。");
            if (keyIv == null)
                throw new ArgumentNullException(nameof(keyIv), @"不能为空。");

            keyBytes = CheckByteArrayLength(keyBytes);
            keyIv = CheckByteArrayLength(keyIv);

            var provider = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIv),
                CryptoStreamMode.Write);

            cStream.Write(sourceBytes, 0, sourceBytes.Length);
            cStream.FlushFinalBlock();

            var buffer = mStream.ToArray();

            mStream.Close();
            cStream.Close();

            return buffer;
        }

        /// <summary>
        /// 密钥长度，如果不是8的倍数或长度大于64则截取前8个元素
        /// </summary>
        private static byte[] CheckByteArrayLength(byte[] byteArray)
        {
            var resultBytes = new byte[8];

            if (byteArray.Length < 8) return Encoding.UTF8.GetBytes("12345678");

            if (byteArray.Length%8 == 0 && byteArray.Length <= 64) return byteArray;

            Array.Copy(byteArray, 0, resultBytes, 0, 8);
            return resultBytes;
        }
    }
}