using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NoSqlCoreService.MongoDbAccessor
{
    /// <summary>
    /// 最一个初始化表达式中的据有赋值的属性。
    /// </summary>
    internal class InitExpresPropAnalysiser
    {
        //成员初始表达式的层次信息队列，用于遍历表达式。
        private readonly Queue<MemberBindingWithStorey> _theQueue = new Queue<MemberBindingWithStorey>();

        //结析结果，解析过程的中间值和解析结果的最终值。
        private readonly List<MemberInform> _propRoutList = new List<MemberInform>();

        /// <summary>
        /// 开始解析。
        /// </summary>
        /// <param name="memInitExpr">要解析的表达示。</param>
        /// <returns>成员信息列表。</returns>
        public IEnumerable<MemberInform> Analysiser(MemberInitExpression memInitExpr)
        {
            var bindWithStorey = new MemberBindingWithStorey()
            {
                Storey = "",
                TheExpression = memInitExpr
            };

            _theQueue.Enqueue(bindWithStorey);

            AnalysisExpres();

            return _propRoutList;
        }

        /// <summary>
        /// 执行解析。
        /// </summary>
        private void AnalysisExpres()
        {
            while (true)
            {
                if (_theQueue.Count > 0)
                {
                    var bindWithStorey = _theQueue.Dequeue();

                    foreach (var memBind in bindWithStorey.TheExpression.Bindings)
                    {
                        //只取成员赋值表达式。忽略其余。
                        if (memBind is MemberAssignment)
                        {
                            var memAss = memBind as MemberAssignment;
                            var thisStorey = bindWithStorey.Storey + "." + memAss.Member.Name;

                            //如果成员赋值表达式的子表达式是一个成员创建表达式。则记录其层次，并按层次解析。
                            //如果不是则可能是常量值、变量值、各种运算符表达式或调用方法的返回值，则直接计算其结果。
                            if (memAss.Expression.NodeType == ExpressionType.MemberInit)
                            {
                                var membInitExp = memAss.Expression as MemberInitExpression;
                                var aStorey = new MemberBindingWithStorey()
                                {
                                    Storey = thisStorey,
                                    TheExpression = membInitExp
                                };
                                _theQueue.Enqueue(aStorey);
                            }
                            else
                            {
                                var thisMebbVal = new MemberInform();
                                thisStorey = thisStorey.Trim('.');

                                thisMebbVal.MemberRoute = thisStorey;
                                thisMebbVal.ValueType = memAss.Expression.Type;

                                var value = TranslateToDirectValue(memAss);

                                thisMebbVal.Value = value;
                                _propRoutList.Add(thisMebbVal);
                            }
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 计算可直接计算的表达式的值。
        /// </summary>
        /// <param name="expression">要解析的表达式。</param>
        /// <returns>解析结果。</returns>
        private static object TranslateToDirectValue(MemberAssignment expression)
        {
            Expression exp;

            if (expression.Expression.NodeType == ExpressionType.Constant)
            {
                exp = expression.Expression;
            }

            var lambda = Expression.Lambda(expression.Expression);
            var fn = lambda.Compile();
            exp = Expression.Constant(fn.DynamicInvoke(null), expression.Expression.Type);

            var constExpr = (ConstantExpression) exp;
            return constExpr.Value;
        }
    }

    /// <summary>
    /// 成员初始化表达式的层次信息。用于解析成员初始表达式
    /// 的过程之中。
    /// </summary>
    internal class MemberBindingWithStorey
    {
        /// <summary>
        /// 层次。
        /// </summary>
        public string Storey { get; set; }

        /// <summary>
        /// 层下之下的成员初始表达式。
        /// </summary>
        public MemberInitExpression TheExpression { get; set; }
    }

    /// <summary>
    /// 成员的信息。
    /// </summary>
    internal class MemberInform
    {
        /// <summary>
        /// 成员路由。
        /// </summary>
        public string MemberRoute { get; set; }

        /// <summary>
        /// 成员值。
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 成员类型。
        /// </summary>
        public Type ValueType { get; set; }
    }
}
