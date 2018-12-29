using System;

namespace CommUtils.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class FixedPointFactory
    {
        public static Action<T> Fix<T>(Func<Action<T>, Action<T>> f)
        {
            return x => f(Fix(f))(x);
        }
        public static Action<T1, T2> Fix<T1, T2>(Func<Action<T1, T2>, Action<T1, T2>> f)
        {
            return (x, y) => f(Fix(f))(x, y);
        }
        public static Action<T1, T2, T3> Fix<T1, T2, T3>(Func<Action<T1, T2, T3>, Action<T1, T2, T3>> f)
        {
            return (x, y, z) => f(Fix(f))(x, y, z);
        }

        public static Func<T, TResult> Fix<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f)
        {
            return x => f(Fix(f))(x);
        }
        public static Func<T1, T2, TResult> Fix<T1, T2, TResult>(Func<Func<T1, T2, TResult>, Func<T1, T2, TResult>> f)
        {
            return (x, y) => f(Fix(f))(x, y);
        }
        public static Func<T1, T2, T3, TResult> Fix<T1, T2, T3, TResult>(Func<Func<T1, T2, T3, TResult>, Func<T1, T2, T3, TResult>> f)
        {
            return (x, y, z) => f(Fix(f))(x, y, z);
        }
    }
}
