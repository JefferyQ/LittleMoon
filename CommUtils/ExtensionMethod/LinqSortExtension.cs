using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace CommUtils.ExtensionMethod
{
    /// <summary>
    /// LINQ排序条件构造扩展方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult">如果有多种类型请传递object</typeparam>
    public static class LinqSortExtension<T, TResult> where T : class
    {
        /// <summary>
        /// 构造排序
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="sortCondition"></param>
        /// <returns></returns>
        public static IQueryable<T> CreateSort(IQueryable<T> queryable,
          IReadOnlyList<SortModel<T, TResult>> sortCondition)
        {
            IOrderedQueryable<T> orderqueryable = null;
            if (sortCondition == null || !sortCondition.Any()) return queryable;
            for (var i = 0; i < sortCondition.Count; i++)
            {
                var sortCreate = sortCondition[i];
                if (i == 0)
                {
                    switch (sortCreate.SortType)
                    {
                        case SortType.Asc:
                            orderqueryable = queryable.OrderBy(sortCreate.SortExp);
                            break;
                        case SortType.Desc:
                            orderqueryable = queryable.OrderByDescending(sortCreate.SortExp);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    continue;
                }
                if (orderqueryable == null) continue;
                switch (sortCreate.SortType)
                {
                    case SortType.Asc:
                        orderqueryable = orderqueryable.ThenBy(sortCreate.SortExp);
                        break;
                    case SortType.Desc:
                        orderqueryable = orderqueryable.ThenByDescending(sortCreate.SortExp);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return orderqueryable;
        }
    }

    /// <summary>
    /// 排序实体对象模型
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    /// <typeparam name="TResult">属性结果</typeparam>
    public class SortModel<T, TResult>
    {
        public Expression<Func<T, TResult>> SortExp { get; set; }
        public SortType SortType { get; set; }
    }

    /// <summary>
    /// 排序原则 升序倒序
    /// </summary>
    public enum SortType
    {
        [Description("升序")]
        Asc,
        [Description("降序")]
        Desc
    }
}
