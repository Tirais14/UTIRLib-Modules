using static UTIRLib.Diagnostics.Constants.MessageText;

namespace UTIRLib.Diagnostics
{
    public class ArgumentNullMessage : ConsoleMessage
    {
        public ArgumentNullMessage() :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor, null, "null")
        { }
        public ArgumentNullMessage(string paramName) :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor, paramName, "null")
        { }
        public ArgumentNullMessage(string paramName, string description) :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor, paramName, "null") =>
            AddDescription(description);
    }
}
