using System;
using System.Collections.Generic;

#nullable enable
namespace UTIRLib.StateMachineSystem
{
    public abstract class ReadOnlyState : IReadOnlyState
    {
        protected readonly HashSet<Type> transitions = new();
        protected readonly bool isAbortable;
        protected bool inNormalUpdate;
        protected bool inFixedUpdate;
        protected bool inLateUpdate;
        protected UpdateAttributes updateAttributes;

        public bool IsAbortable => isAbortable;
        public bool InNormalUpdate => inNormalUpdate;
        public bool InFixedUpdate => inFixedUpdate;
        public bool InLateUpdate => inLateUpdate;
        public UpdateAttributes UpdateAttributes => updateAttributes;
        public bool IsCompleted { get; protected set; }
        public bool IsExecuting => !IsCompleted;

        protected ReadOnlyState(IEnumerable<Type> transitions, UpdateAttributes updateAttributes = UpdateAttributes.Normal,
            bool isAbortable = false)
        {
            foreach (Type transition in transitions) {
                this.transitions.Add(transition);
            }
            this.isAbortable = isAbortable;
            SetUpdateAttributes(updateAttributes);
        }
        protected ReadOnlyState(StateParameters stateParameters) : this(stateParameters.transitions,
            stateParameters.updateAttributes, stateParameters.isAbortable)
        { }

        public virtual void Enter() => IsCompleted = false;

        public abstract void Execute();

        public virtual void Exit() => IsCompleted = true;

        public bool CanSwitchTo(IState? state) => CanSwitchTo(state?.GetType());
        public bool CanSwitchTo(Type? stateType) => transitions.Count > 0 && stateType != null && transitions.Contains(stateType);

        protected void SetUpdateAttributes(UpdateAttributes updateAttributes)
        {
            this.updateAttributes = updateAttributes;
            inNormalUpdate = updateAttributes.HasFlag(UpdateAttributes.Normal);
            inFixedUpdate = updateAttributes.HasFlag(UpdateAttributes.Fixed);
            inLateUpdate = updateAttributes.HasFlag(UpdateAttributes.Late);
        }
    }
}
