using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace UTIRLib.StateMachineSystem
{
    public class CompositeState : ReadOnlyState, IReadOnlyComposite<IReadOnlyState>
    {
        protected readonly IReadOnlyState[] states;
        protected readonly int count;

        public int Count => count;
        public IReadOnlyState this[int index] => states[index];

        protected CompositeState(StateParameters stateParameters, params IReadOnlyState[] states)
            : base(stateParameters)
        {
            this.states = states;
            UpdateAttributes mergedUpdateAttributes = MergeUpdateAttributes();
            SetUpdateAttributes(mergedUpdateAttributes);
        }

        public override void Execute()
        {
            for (int i = 0; i < count; i++) {
                states[i].Execute();
            }
        }

        public bool Contains(object? value) => Contains(value as IReadOnlyState);
        public bool Contains(IReadOnlyState? value) => value != null && states.Contains(value);

        public IReadOnlyState GetChild(int index) => states[index];

        public IEnumerator<IReadOnlyState> GetEnumerator() => states.GetEnumeratorT();

        protected UpdateAttributes MergeUpdateAttributes()
        {
            UpdateAttributes mergedUpdateAttributes = updateAttributes;
            for (int i = 0; i < states.Length; i++) {
                mergedUpdateAttributes |= states[i].UpdateAttributes;
            }

            return mergedUpdateAttributes;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        object IReadOnlyComposite.GetChild(int index) => GetChild(index);
    }
}
