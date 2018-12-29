using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommUtils.ExtensionMethod
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> entities, Action<T> action)
        {
            foreach (var entity in entities)
                action(entity);
        }

        public static List<T> ToList<T>(this IEnumerable<T> entities, Func<T, bool> func)
        {
            return entities.Where(func).ToList();
        }

        public static IEnumerable<T> Update<T>(this IEnumerable<T> entities, Action<T> action)
        {
            foreach (var entity in entities)
                action(entity);
            return entities;
        }

        /// <summary>
        /// 隐式迭代，遍历树形所有节点进行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="action"></param>
        /// <param name="props"></param>
        public static void ImplicitIteration<T>(this IEnumerable<T> entities, Action<T> action,
            List<PropertyInfo> props = null)
        {
            if (entities == null) return;
            props = props ?? typeof(T).GetProperties()
                .Where(p => p.PropertyType.GetInterfaces().Any(g => g == typeof(IEnumerable<T>)))
                .ToList();
            if (props.Count == 0)
                ForEach(entities, action);
            else
                entities.ForEach(entity =>
                {
                    action(entity);
                    props.ForEach(p => (p.GetValue(entity) as IEnumerable<T>).ImplicitIteration(action, props));
                });

        }
    }
}