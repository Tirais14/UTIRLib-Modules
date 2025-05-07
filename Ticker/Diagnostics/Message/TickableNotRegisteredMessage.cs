using System;

namespace UTIRLib.CustomTicker
{
    using Diagnostics;
    public class TickableNotRegisteredMessage : ConsoleMessage
    {
        public TickableNotRegisteredMessage(object obj) :
            base($"Object {obj?.GetType().Name ?? "null"} cannot be registered.",
                CallStackFramesOffsetConstructor)
        { }
        public TickableNotRegisteredMessage(object obj, string description) : 
            base($"Object {obj?.GetType().Name ?? "null"} cannot be registered.",
                CallStackFramesOffsetConstructor) => AddDescription(description);
        public TickableNotRegisteredMessage(Type type) :
            base($"Object {type?.Name ?? "null"} cannot be registered.",
                CallStackFramesOffsetConstructor)
        { }
        public TickableNotRegisteredMessage(Type type, string description) :
            base($"Object {type?.Name ?? "null"} cannot be registered.",
                CallStackFramesOffsetConstructor) => AddDescription(description);
    }
}