using System;

namespace CommUtils.Attributes
{
    /// <summary>
    /// 接口实现类具体类型
    /// 该类型的完全限定名称，包括其命名空间及程序集。
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class ConcreteTypeAttribute : Attribute
    {
        /// <summary>
        /// 带参构造函数
        /// </summary>
        /// <param name="typeFullQualifiedName">实现类完全限定名</param>
        public ConcreteTypeAttribute(string typeFullQualifiedName)
        {
            TypeFullQualifiedName = typeFullQualifiedName;
        }

        /// <summary>
        /// 实现类完全限定名
        /// 该类型的完全限定名称，包括其命名空间，但不包括程序集。
        /// </summary>
        public string TypeFullQualifiedName
        {
            get; private set;
        }
    }
}
