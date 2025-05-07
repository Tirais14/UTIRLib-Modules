using System;
using static UTIRLib.Diagnostics.Constants.MessageText;
using static UTIRLib.Diagnostics.DiagnosticsUtility;


namespace UTIRLib.Diagnostics.Exceptions
{
    public class NullOrEmptyStringInCollectionException : Exception
    {
        public NullOrEmptyStringInCollectionException() :
            base(string.Format(COLLECTION_CANNOT_NOT_CONTAIN_MESSAGE, null, NULL_OR_EMPTY_TEXT,
                "string"))
        { }
        public NullOrEmptyStringInCollectionException(string paramName) :
             base(string.Format(COLLECTION_CANNOT_NOT_CONTAIN_MESSAGE, 
                 ToFormattedParameterName(paramName), NULL_OR_EMPTY_TEXT, "string"))
        { }
        public NullOrEmptyStringInCollectionException(string param, string paramName) :
            base(string.Format(COLLECTION_CANNOT_NOT_CONTAIN_MESSAGE, 
                ToFormattedParameterName(paramName), ResolveNullOrEmptyText(param), "string"))
        { }
    }
}
