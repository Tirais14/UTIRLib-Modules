#nullable enable
using System;

namespace UTIRLib.StateMachineSystem
{
    public abstract class MonoStateMachine : MonoInitializable, IStateMachine
    {
        protected IStateMachine stateMachineInternal = null!;

        public IReadOnlyState PlayingState => stateMachineInternal.PlayingState;
        public bool IsExecuting => stateMachineInternal.IsExecuting;
        public bool IsIdle => stateMachineInternal.IsIdle;
        public bool IsEnabled { get; set; } = false;

        protected override void InitializeInternal()
        {
            stateMachineInternal = GetStateMachine(GetIdleState(), GetStates(), GetTransitions());
            IsEnabled = true;
        }

        public void Execute() => stateMachineInternal.Execute();

        public void SwitchTo(Type stateType) => stateMachineInternal.SwitchTo(stateType);
        public void SwitchTo(IReadOnlyState state) => stateMachineInternal.SwitchTo(state);

        public IReadOnlyState GetState(Type type) => stateMachineInternal.GetState(type);
        public TState GetState<TState>() => stateMachineInternal.GetState<TState>();

        public void AddState(IReadOnlyState state) => stateMachineInternal.AddState(state);

        public void AddStates(params IReadOnlyState[] states) => stateMachineInternal.AddStates(states);

        public void AddTransition(StateTransition transition) => stateMachineInternal.AddTransition(transition);

        public void AddTransitions(params StateTransition[] transitions) => stateMachineInternal.AddTransitions(transitions);

        public void RemoveState(IReadOnlyState? state) => stateMachineInternal.RemoveState(state);

        public void RemoveStates(params IReadOnlyState?[] states) => stateMachineInternal.RemoveStates(states);

        public void RemoveTransition(StateTransition transition) => stateMachineInternal.RemoveTransition(transition);

        public void RemoveTransitions(params StateTransition[] transitions) => stateMachineInternal.RemoveTransitions(transitions);

        public bool IsCompleted(Type type) => stateMachineInternal.IsCompleted(type);

        public bool Contains(Type? type) => stateMachineInternal.Contains(type);
        public bool Contains(IReadOnlyState? state) => stateMachineInternal.Contains(state);
        public bool Contains(StateTransition transition) => throw new NotImplementedException();

        public bool IsStateExecuting(Type? type) => stateMachineInternal.IsStateExecuting(type);
        public bool IsStateExecuting(IReadOnlyState? state) => stateMachineInternal.IsStateExecuting(state);

        protected void OnUpdate()
        {
            if (IsEnabled && stateMachineInternal.PlayingState.InNormalUpdate) {
                stateMachineInternal.Execute();
            }
        }

        protected void OnFixedUpdate()
        {
            if (IsEnabled && stateMachineInternal.PlayingState.InFixedUpdate) {
                stateMachineInternal.Execute();
            }
        }

        protected void OnLateUpdate()
        {
            if (IsEnabled && stateMachineInternal.PlayingState.InLateUpdate) {
                stateMachineInternal.Execute();
            }
        }

        protected virtual IStateMachine GetStateMachine(IReadOnlyState idleState, IReadOnlyState[] states,
            StateTransition[] transitions) => new StateMachine(idleState, states, transitions);

        protected abstract IReadOnlyState GetIdleState();

        protected abstract IReadOnlyState[] GetStates();

        protected abstract StateTransition[] GetTransitions();
    }
}
