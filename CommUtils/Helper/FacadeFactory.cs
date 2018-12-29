namespace CommUtils.Helper
{
    public class FacadeFactory
    {
        //private static readonly ConcurrentDictionary<string, Type> Facade = new ConcurrentDictionary<string, Type>();

        public static T CreateFacade<T>() where T : new()
        {
            return new T();
            //var fullName = typeof (T).FullName;
            //var type = Facade.GetOrAdd(fullName, key =>
            //{
            //    var attrs = typeof (T).GetCustomAttributes(typeof (ConcreteTypeAttribute), true);
            //    if (attrs.Length <= 0) return typeof (T);
            //    var facadeName = ((ConcreteTypeAttribute) attrs[0]).TypeFullQualifiedName;
            //    var targetType = Type.GetType(facadeName);
            //    Facade[key] = targetType;
            //    return targetType;
            //});
            //return (T) Activator.CreateInstance(type);
        }
    }
}