using static UTIRLib.Diagnostics.Constants.MessageText;
using static UTIRLib.Diagnostics.DiagnosticsUtility;

namespace UTIRLib.Diagnostics
{
    public class NullOrEmptyCollectionMessage : ConsoleMessage
    {
        public NullOrEmptyCollectionMessage() :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor,
                null, NULL_OR_EMPTY_TEXT, "collection")
        { }
        public NullOrEmptyCollectionMessage(string paramName) :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor,
                paramName, NULL_OR_EMPTY_TEXT, "collection")
        { }
        public NullOrEmptyCollectionMessage(string paramName, string description) :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor,
                paramName, NULL_OR_EMPTY_TEXT, "collection") => AddDescription(description);
        public NullOrEmptyCollectionMessage(object param, string paramName) :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor,
                paramName, ResolveNullOrEmptyText(param), "collection")
        { }
        public NullOrEmptyCollectionMessage(object param, string paramName, string description) :
            base(VALUE_CANNOT_NOT_CONTAIN_MESSAGE, CallStackFramesOffsetConstructor,
                paramName, ResolveNullOrEmptyText(param), "collection") => AddDescription(description);
    }
}
