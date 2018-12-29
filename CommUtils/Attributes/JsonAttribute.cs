using System;

namespace CommUtils.Attributes
{
    public class JsonAttribute : Attribute
    {
        public bool Ignore { get; private set; }

        public JsonAttribute(bool ignore)
        {
            Ignore = ignore;
        }
    }
}
