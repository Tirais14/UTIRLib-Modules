using UnityEngine;
using UTIRLib;
using UTIRLib.Diagnostics;

namespace UTIRLib.Database
{
    public sealed class NotFoundException : UtirlibException
    {
        private const string MESSAGE = "Not found{0} in database registry.";

        public NotFoundException() : base(MESSAGE, " item")
        { }
        public NotFoundException(object value) : base(MESSAGE, $" value {value.GetTypeName()}")
        { }
    }
}
