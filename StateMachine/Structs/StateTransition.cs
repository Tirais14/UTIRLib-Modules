using System;

#nullable enable
namespace UTIRLib.StateMachineSystem
{
    public readonly struct StateTransition
    {
        private readonly Func<bool> predicate;
        public readonly Type nextStateType;
        public readonly bool canAbortPrevious;

        public bool PredicateValue => predicate();

        public StateTransition(Func<bool> predicate, Type nextStateType, bool canAbortPrevious)
        {
            this.predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            this.nextStateType = nextStateType ?? throw new ArgumentNullException(nameof(nextStateType));
            this.canAbortPrevious = canAbortPrevious;
        }
        public StateTransition(Func<bool> predicate, Type nextStateType) : this(predicate, nextStateType, false)
        {
        }
    }
}
