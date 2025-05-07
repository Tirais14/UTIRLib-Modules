#nullable enable
using System;

namespace UTIRLib.StateMachineSystem
{
    public interface IReadOnlyState : IExecutable
    {
        UpdateAttributes UpdateAttributes { get; }
        bool IsCompleted { get; }
        bool IsAbortable { get; }
        bool InNormalUpdate { get; }
        bool InFixedUpdate { get; }
        bool InLateUpdate { get; }

        void Enter();

        void Exit();

        bool CanSwitchTo(Type? stateType);
        bool CanSwitchTo(IState? state);
    }
}
