using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace CommUtils
{
    public static class ConvertHelper
    {
        /// <summary>
        /// 将原数据转为目标数据
        /// 注：支持批量,但只支持列表接口子类,如：List
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="t"></param>
        /// <param name="defaultMapConfig"></param>
        /// <returns></returns>
        public static TTo ConvertToTResult<TFrom, TTo>(TFrom t, DefaultMapConfig defaultMapConfig = null)
        {
            var mapper =
                ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(defaultMapConfig ?? new DefaultMapConfig());
            return mapper.Map(t);
        }

        public static TTo ConvertTo<TFrom, TTo>(this TFrom t, DefaultMapConfig defaultMapConfig = null)
        {
            var mapper =
                ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>(defaultMapConfig ?? new DefaultMapConfig());
            return mapper.Map(t);
        }

        public static T Convert<T>(DataTable tb) where T : class
        {
            var lst = DataTableToEntity<T>(tb);
            return lst.Any() ? lst.FirstOrDefault() : default(T);
        }
        public static List<T> ConvertEnumToList<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 将datatable转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tb"></param>
        /// <returns></returns>
        public static IList<T> DataTableToEntity<T>(DataTable tb) where T : class
        {
            if (!HasRows(tb))
                return new List<T>();
            var lst = new List<T>();
            foreach (DataRow item in tb.Rows)
            {
                var t = Activator.CreateInstance<T>();
                foreach (DataColumn col in tb.Columns)
                {
                    //先判断属性
                    var pi = t.GetType()
                        .GetProperty(col.ColumnName,
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    //再判断列
                    var objValue = item[col.ColumnName];
                    if (pi != null
                        && pi.PropertyType == col.DataType
                        && pi.CanWrite && (objValue != null
                                           && objValue != DBNull.Value))
                    {
                        pi.SetValue(t, objValue, null);
                    }
                }
                lst.Add(t);
            }
            return lst;
        }

        /// <summary>
        /// 将列头含有特殊字符的datatable转为实体
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="tb">原数据table</param>
        /// <returns></returns>
        public static IList<T> DataTableToEntityWithColumnChar<T>(DataTable tb) where T : class
        {
            if (!HasRows(tb))
                return new List<T>();
            var lst = new List<T>();
            foreach (DataRow item in tb.Rows)
            {
                var t = Activator.CreateInstance<T>();
                foreach (DataColumn col in tb.Columns)
                {
                    var tbColumnName = col.ColumnName;
                    var entityPropertyName = ReplaceSpecialChar(tbColumnName);
                    //先判断属性
                    var pi = t.GetType()
                        .GetProperty(entityPropertyName,
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    //再判断列
                    var objValue = item[tbColumnName];
                    if (pi != null
                        && pi.PropertyType == col.DataType
                        && pi.CanWrite && (objValue != null
                                           && objValue != DBNull.Value))
                    {
                        pi.SetValue(t, objValue, null);
                    }
                }
                lst.Add(t);
            }
            return lst;
        }

        /// <summary>
        /// 去掉特殊字符
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private static string ReplaceSpecialChar(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                return columnName;
            var empty = string.Empty;
            return columnName.Replace("~", empty)
                .Replace("!", empty)
                .Replace("@", empty)
                .Replace("#", empty)
                .Replace("$", empty)
                .Replace("%", empty)
                .Replace("^", empty)
                .Replace("&", empty)
                .Replace("*", empty)
                .Replace("(", empty)
                .Replace(")", empty);
        }

        /// <summary>
        /// 检查table是否存在行
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private static bool HasRows(DataTable tb)
        {
            return tb != null && tb.Rows.Count > 0;
        }

        /// <summary>
        /// 将类的数据转换到同类中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T AutoCopy<T>(T parent) where T : class, new()
        {
            var obj = new T();
            var tObj = typeof(T);
            tObj.GetProperties().ToList().ForEach(t =>
            {
                if (t.CanRead && t.CanWrite)
                    t.SetValue(obj, t.GetValue(parent, null), null);
            });
            return obj;
        }
    }
}