using UTIRLib.Diagnostics;

namespace UTIRLib.AnimatorSystem.Diagnostics
{
    public class CustomAnimatorStateMachineTransitionMessage : ConsoleMessage
    {
        public CustomAnimatorStateMachineTransitionMessage() :
            base($"Transition failed.", CallStackFramesOffsetConstructor)
        { }
        public CustomAnimatorStateMachineTransitionMessage(string description) :
            base($"Transition failed. {description}", CallStackFramesOffsetConstructor)
        { }
    }
}