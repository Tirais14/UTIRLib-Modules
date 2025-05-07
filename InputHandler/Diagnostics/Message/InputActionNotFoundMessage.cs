using System;
using UTIRLib.Diagnostics;

#nullable enable
namespace UTIRLib.InputSystem.Diagnostics
{
    public class InputActionNotFoundMessage : ConsoleMessage
    {
        public InputActionNotFoundMessage() :
            base("Specified input action not found.", CallStackFramesOffsetConstructor)
        { }
        public InputActionNotFoundMessage(Enum control) :
            base($"Specified input action {Enum.GetName(control.GetType(), control)} not found.",
                CallStackFramesOffsetConstructor)
        { }
        public InputActionNotFoundMessage(string control) :
            base($"Specified input action {control} not found.", CallStackFramesOffsetConstructor)
        { }
        public InputActionNotFoundMessage(Enum control, Type inputValueType) :
            base($"Specified input action {control} ({inputValueType.Name}) not found.", CallStackFramesOffsetConstructor)
        { }
        public InputActionNotFoundMessage(string control, Type inputValueType) :
            base($"Specified input action {control} ({inputValueType.Name}) not found.", CallStackFramesOffsetConstructor)
        { }
    }
}