using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CommUtils.ExtensionMethod
{
    /// <summary>
    /// 表达式树扩展方法类
    /// </summary>
    //public static class ExpressionExtension
    //{
    //    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
    //        Expression<Func<T, bool>> expr2)
    //    {
    //        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2.Body), expr1.Parameters);
    //    }

    //    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
    //        Expression<Func<T, bool>> expr2)
    //    {
    //        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, expr2.Body), expr1.Parameters);
    //    }
    //}
    /// <summary>
    /// 表达式树扩展方法类
    /// </summary>
    public static class PredicateBuilder
    {

        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
            Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

    /// <summary>
    /// 表达式类型转换
    /// </summary>
    public static class LambdaExpressionConvert
    {
        /// <summary>
        /// 表达式类型转换器
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <param name="expression">表达式内容</param>
        /// <returns></returns>
        private static Expression Parser(ParameterExpression parameter, Expression expression)
        {
            if (expression == null) return null;
            switch (expression.NodeType)
            {
                //一元运算符
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                {
                    var unary = expression as UnaryExpression;
                    if (unary == null)
                        throw new ArgumentException("unary is null");
                    var exp = Parser(parameter, unary.Operand);
                    return Expression.MakeUnary(expression.NodeType, exp, unary.Type, unary.Method);
                }
                //二元运算符
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                {
                    var binary = expression as BinaryExpression;
                    if (binary == null)
                        throw new ArgumentException("binary is null");
                    var left = Parser(parameter, binary.Left);
                    var right = Parser(parameter, binary.Right);
                    var conversion = Parser(parameter, binary.Conversion);
                    if (binary.NodeType == ExpressionType.Coalesce)
                        return Expression.Coalesce(left, right, conversion as LambdaExpression);
                    return Expression.MakeBinary(expression.NodeType, left, right, binary.IsLiftedToNull, binary.Method);
                }
                //其他
                case ExpressionType.Call:
                {
                    var call = expression as MethodCallExpression;
                    if (call == null)
                        throw new ArgumentException("call is null");
                    var arguments = call.Arguments.Select(argument => Parser(parameter, argument)).ToList();
                    var instance = Parser(parameter, call.Object);
                    call = Expression.Call(instance, call.Method, arguments);
                    return call;
                }
                case ExpressionType.Lambda:
                {
                    var lambda = expression as LambdaExpression;
                    if (lambda == null)
                        throw new ArgumentException("lambda is null");
                    return Parser(parameter, lambda.Body);
                }
                case ExpressionType.MemberAccess:
                {
                    var memberAccess = expression as MemberExpression;
                    if (memberAccess == null)
                        throw new ArgumentException("memberAccess is null");
                    if (memberAccess.Expression == null)
                        memberAccess = Expression.MakeMemberAccess(null, memberAccess.Member);
                    else
                    {
                        var exp = Parser(parameter, memberAccess.Expression);
                        var member = exp.Type.GetMember(memberAccess.Member.Name).FirstOrDefault();
                        if (member == null)
                            throw new ArgumentException($"目标类型没有该属性【{memberAccess.Member.Name}】");
                        memberAccess = Expression.MakeMemberAccess(exp, member);
                    }
                    return memberAccess;
                }
                case ExpressionType.Parameter:
                    return parameter;
                case ExpressionType.Constant:
                    return expression;
                case ExpressionType.TypeIs:
                {
                    var typeis = expression as TypeBinaryExpression;
                    if (typeis == null)
                        throw new ArgumentException("memberAccess is null");
                    var exp = Parser(parameter, typeis.Expression);
                    return Expression.TypeIs(exp, typeis.TypeOperand);
                }
                default:
                    throw new Exception($"Unhandled expression type: '{expression.NodeType}'");
            }
        }

        /// <summary>
        /// 表达式树模型反射转换方法
        /// </summary>
        /// <typeparam name="TInput">原数据类型</typeparam>
        /// <typeparam name="TToProperty">目标数据类型</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns></returns>
        public static Expression<Func<TToProperty, bool>> Cast<TInput, TToProperty>(
            this Expression<Func<TInput, bool>> expression)
        {
            var p = Expression.Parameter(typeof (TToProperty), "p");
            var x = Parser(p, expression);
            return Expression.Lambda<Func<TToProperty, bool>>(x, p);
        }
    }
}