using System;
using static UTIRLib.Diagnostics.Constants.MessageText;
using static UTIRLib.Diagnostics.DiagnosticsUtility;

namespace UTIRLib.Diagnostics.Exceptions
{
    public class NullOrEmptyCollectionException : Exception
    {
        public NullOrEmptyCollectionException() : 
            base(string.Format(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, null, NULL_OR_EMPTY_TEXT, "collection"))
        { }
        public NullOrEmptyCollectionException(string paramName) :
            base(string.Format(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, ToFormattedParameterName(paramName),
                NULL_OR_EMPTY_TEXT, "collection"))
        { }
        public NullOrEmptyCollectionException(object param, string paramName) :
            base(string.Format(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, ToFormattedParameterName(paramName),
                ResolveNullOrEmptyText(param), "collection"))
        { }
    }
}
