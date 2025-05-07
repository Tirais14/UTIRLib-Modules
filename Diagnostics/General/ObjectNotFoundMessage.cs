using System;
using static UTIRLib.Diagnostics.Constants.MessageText;
using static UTIRLib.Diagnostics.DiagnosticsUtility;

namespace UTIRLib.Diagnostics
{
    public class ObjectNotFoundMessage : ConsoleMessage
    {
        public ObjectNotFoundMessage() :
            base(NOT_FOUND_MESSAGE, CallStackFramesOffsetConstructor, "Unity object")
        { }
        public ObjectNotFoundMessage(Type type) :
            base(NOT_FOUND_MESSAGE, CallStackFramesOffsetConstructor, "Unity object", type.Name)
        { }
        public ObjectNotFoundMessage(string paramName) :
            base(NOT_FOUND_MESSAGE, CallStackFramesOffsetConstructor, "Unity object", null, paramName)
        { }
        public ObjectNotFoundMessage(string paramName, Type type) :
            base(NOT_FOUND_MESSAGE, CallStackFramesOffsetConstructor, "Unity object", type.Name, paramName)
        { }
    }
}