namespace UTIRLib.CustomTicker
{
    using Diagnostics;
    public class TickableAlreadyRegisteredMessage : ConsoleMessage
    {
        public TickableAlreadyRegisteredMessage(object obj) :
            base($"Object {obj?.GetType().Name ?? "null"} already registered.",
                CallStackFramesOffsetConstructor)
        { }
    }
}