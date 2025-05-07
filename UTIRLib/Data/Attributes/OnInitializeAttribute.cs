using System;

namespace UTIRLib.Attributes
{
    /// <summary>
    /// Uses for mark method, which will be invoked by <see cref="IInitializable"/> interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class OnInitializeAttribute : Attribute
    {
        public int Order { get; private set; } = 0;

        public OnInitializeAttribute() { }
        public OnInitializeAttribute(int order) => Order = order;
    }
}
