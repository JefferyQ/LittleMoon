using System;
using CommUtils.Exceptions;

namespace CommUtils
{
    public static class ThrowHelper
    {        /// <summary>
             /// 抛出参数异常
             /// </summary>
             /// <param name="msg"></param>
             /// <returns></returns>
        public static ArgumentException CreateArgumentException(string msg)
        {
            return new ArgumentException(msg);
        }

        public static ArgumentException CreateArgumentNullException(string paramName)
        {
            return new ArgumentNullException(paramName);
        }

        public static ArgumentException CreateArgumentException(string msg, string paramName)
        {
            return new ArgumentException(msg, paramName);
        }

        /// <summary>
        /// 抛出登陆异常
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static LoginException CreateLoginException(string msg)
        {
            return new LoginException(msg);
        }

        /// <summary>
        /// 抛出权限异常
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static AuthorizationException CreateAuthorizationException(string msg)
        {
            return new AuthorizationException(msg);
        }

        /// <summary>
        /// 抛出WebApi自定义异常
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static WebApiException CreateWebApiException(string msg)
        {
            return new WebApiException(msg);
        }
    }
}
