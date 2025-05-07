using System;
using static UTIRLib.Diagnostics.Constants.MessageText;
using static UTIRLib.Diagnostics.DiagnosticsUtility;

namespace UTIRLib.Diagnostics.Exceptions
{
    public class NullOrEmptyStringException : Exception
    {
        public NullOrEmptyStringException() :
            base(string.Format(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, null, NULL_OR_EMPTY_TEXT, "string"))
        { }
        public NullOrEmptyStringException(string paramName) :
            base(string.Format(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, 
                ToFormattedParameterName(paramName), NULL_OR_EMPTY_TEXT, "string"))
        { }
        public NullOrEmptyStringException(string parameter, string paramName) :
           base(string.Format(VALUE_CANNOT_NOT_CONTAIN_MESSAGE,
               ToFormattedParameterName(paramName), ResolveNullOrEmptyText(parameter), "string"))
        { }
    }
}
