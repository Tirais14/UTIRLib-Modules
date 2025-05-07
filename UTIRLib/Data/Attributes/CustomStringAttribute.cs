using System;

namespace UTIRLib.Attributes
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, Inherited = false)]
    public class CustomStringAttribute : Attribute
    {
        public string Value { get; private set; }

        public CustomStringAttribute(string value) => Value = value;
    }
}
