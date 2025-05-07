using System;
using static UTIRLib.Diagnostics.Constants.MessageText;
using static UTIRLib.Diagnostics.DiagnosticsUtility;

namespace UTIRLib.Diagnostics.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException() :
            base(string.Format(NOT_FOUND_MESSAGE, "Object", null, null))
        { }
        public ObjectNotFoundException(string paramName) :
            base(string.Format(NOT_FOUND_MESSAGE, "Object", ToFormattedParameterName(paramName), null))
        { }
        public ObjectNotFoundException(Type objectType) :
            base(string.Format(NOT_FOUND_MESSAGE, ResolveTypeNameBySystemType(objectType), null, null))
        { }
        public ObjectNotFoundException(string paramName, Type objectType) :
            base(string.Format(NOT_FOUND_MESSAGE, ResolveTypeNameBySystemType(objectType),
                ToFormattedParameterName(paramName), null))
        { }
    }
}