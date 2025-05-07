using System;

namespace UTIRLib.Diagnostics
{
    public class InvalidTypeCastMessage : ConsoleMessage
    {
        public InvalidTypeCastMessage() :
            base("Invalid type cast operation.", CallStackFramesOffsetConstructor)
        { }
        public InvalidTypeCastMessage(string paramName) :
            base($"Invalid type cast operation. Value {paramName}", CallStackFramesOffsetConstructor)
        { }
        public InvalidTypeCastMessage(string paramName, Type originalType, Type castType) :
            base($"Invalid type cast operation from {originalType.Name} to {castType.Name}. " +
                $"Value {paramName}", CallStackFramesOffsetConstructor)
        { }
    }
}
