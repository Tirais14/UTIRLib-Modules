namespace UTIRLib.Diagnostics
{
    public class ArgumentWrongMessage : ConsoleMessage
    {
        public ArgumentWrongMessage() : base("Wrong argument.", CallStackFramesOffsetConstructor)
        { }
        public ArgumentWrongMessage(string paramName) :
            base($"Wrong argument. Value {paramName}", CallStackFramesOffsetConstructor)
        { }
        public ArgumentWrongMessage(object param, string paramName) :
            base($"Wrong argument. Value {paramName}={param}", CallStackFramesOffsetConstructor)
        { }
        public ArgumentWrongMessage(object param, string paramName, string description) :
            base($"Wrong argument. Value {paramName}={param}.", CallStackFramesOffsetConstructor) =>
            AddDescription(description);
    }
}
