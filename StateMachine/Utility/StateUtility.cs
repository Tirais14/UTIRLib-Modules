using System;

namespace UTIRLib.StateMachineSystem
{
    public static class StateUtility
    {
#nullable enable
        public static bool IsIdleState(Type type) => type.Name.ToLower().Contains("idle");
        public static bool IsIdleState(IState state) => IsIdleState(state.GetType());

        public static bool IsNotIdleState(Type type) => !IsIdleState(type);
        public static bool IsNotIdleState(IState state) => !IsIdleState(state.GetType());
    }
}
