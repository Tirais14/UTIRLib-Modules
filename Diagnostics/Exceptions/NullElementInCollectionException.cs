using System;
using static UTIRLib.Diagnostics.Constants.MessageText;
using static UTIRLib.Diagnostics.DiagnosticsUtility;

namespace UTIRLib.Diagnostics.Exceptions
{
    public class NullElementInCollectionException : Exception
    {
        public NullElementInCollectionException() :
            base(string.Format(COLLECTION_CANNOT_NOT_CONTAIN_MESSAGE, null, "null", "element"))
        { }
        public NullElementInCollectionException(string paramName) :
            base(string.Format(VALUE_CANNOT_NOT_CONTAIN_MESSAGE,
                ToFormattedParameterName(paramName), "null", "element"))
        { }
    }
}
