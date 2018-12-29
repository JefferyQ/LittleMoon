using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommUtils.Helper
{
    public class TasksHelper
    {
        /// <summary>
        /// 将一个方法function异步运行，在执行完毕时执行回调callback
        /// </summary>
        /// <param name="function">异步方法，该方法没有参数，返回类型必须是void</param>
        /// <param name="callback">异步方法执行完毕时执行的回调方法，该方法没有参数，返回类型必须是void</param>
        public static async void RunAsync(Action function, Action callback = null)
        {
            await Task.Run(function);
            callback?.Invoke();
        }

        /// <summary>
        /// 将一个方法function异步运行，在执行完毕时执行回调callback
        /// </summary>
        /// <typeparam name="TResult">异步方法的返回类型</typeparam>
        /// <param name="function">异步方法，该方法没有参数，返回类型必须是TResult</param>
        /// <param name="callback">异步方法执行完毕时执行的回调方法，该方法参数为TResult，返回类型必须是void</param>
        public static async void RunAsync<TResult>(Func<TResult> function, Action<TResult> callback = null)
        {
            var rlt = await Task.Run(function);
            callback?.Invoke(rlt);
        }

        /// <summary>
        /// 同步执行委托，如果失败则默认暂停一毫秒再次执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="maxCount"></param>
        /// <param name="sleepTime"></param>
        /// <returns></returns>
        public T TryForCount<T>(Func<T> fun, int maxCount = 1, int sleepTime = 1)
        {
            while (maxCount > 0)
            {
                try
                {
                    return fun();
                }
                catch (Exception)
                {
                    maxCount--;
                    if (maxCount <= 0)
                        throw;
                    Thread.Sleep(sleepTime);
                }
            }
            return default(T);
        }
    }
}