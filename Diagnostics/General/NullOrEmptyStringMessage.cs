using static UTIRLib.Diagnostics.Constants.MessageText;
using static UTIRLib.Diagnostics.DiagnosticsUtility;

namespace UTIRLib.Diagnostics
{
    public class NullOrEmptyStringMessage : ConsoleMessage
    {
        public NullOrEmptyStringMessage() :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor, null,
                NULL_OR_EMPTY_TEXT, "string")
        { }
        public NullOrEmptyStringMessage(string paramName) :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor,
                NULL_OR_EMPTY_TEXT, "string")
        { }
        public NullOrEmptyStringMessage(string str, string paramName) :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor, paramName,
                ResolveNullOrEmptyText(str), "string")
        { }
    }
}
