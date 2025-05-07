using System;
using static UTIRLib.Diagnostics.Constants.MessageText;

namespace UTIRLib.Diagnostics
{
    public class StateNotFoundMessage : ConsoleMessage
    {
        public StateNotFoundMessage() : base(NOT_FOUND_MESSAGE, CallStackFramesOffsetConstructor, "State", null)
        { }
        public StateNotFoundMessage(string stateName) : base(NOT_FOUND_MESSAGE, CallStackFramesOffsetConstructor, "State",
            stateName)
        { }
        public StateNotFoundMessage(string stateName, string stateMachineName) : base(NOT_FOUND_MESSAGE,
            CallStackFramesOffsetConstructor, "State", stateName)
        { }
        public StateNotFoundMessage(string stateName, Type stateMachineType) : this(stateName, stateMachineType.Name)
        { }
        public StateNotFoundMessage(Type stateType, Type stateMachineType) : this(stateType.Name, stateMachineType.Name)
        { }
        public StateNotFoundMessage(Type stateType, object stateMachine) : this(stateType, stateMachine.GetType())
        { }
        public StateNotFoundMessage(object state, object stateMachine) : this(state.GetType(), stateMachine)
        { }
    }
}
