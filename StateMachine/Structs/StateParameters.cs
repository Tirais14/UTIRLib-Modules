#nullable enable
using System;
using System.Linq;

namespace UTIRLib.StateMachineSystem
{
    public struct StateParameters
    {
        public UpdateAttributes updateAttributes;
        public bool isAbortable;
        public Type[] transitions;

        public StateParameters(UpdateAttributes updateAttributes, bool isAbortable, params Type[] transitions)
        {
            this.updateAttributes = updateAttributes;
            this.isAbortable = isAbortable;
            this.transitions = transitions;
        }
        public StateParameters(UpdateAttributes updateAttributes, params Type[] transitions) : this(updateAttributes, false, transitions)
        {
        }
        public StateParameters(bool isAbortable, params Type[] transitions) : this(UpdateAttributes.Normal, isAbortable, transitions)
        {
        }
        public StateParameters(params Type[] transitions) : this(UpdateAttributes.Normal, false, transitions)
        {
        }

        public void SetTransitions(params Type[] stateTypes) =>  transitions = stateTypes;
        public void SetTransitions(params IReadOnlyState[] states) => SetTransitions(states.Select((value) => value.GetType()).ToArray());

        public static StateParameters CreateNormal(params Type[] stateTypes) => new(UpdateAttributes.Normal, false, stateTypes);
        public static StateParameters CreateAbortableNormal(params Type[] stateTypes) => new(UpdateAttributes.Normal, true, stateTypes);

        public static StateParameters CreateFixed(params Type[] stateTypes) => new(UpdateAttributes.Fixed, false, stateTypes);
        public static StateParameters CreateAbortableFixed(params Type[] stateTypes) => new(UpdateAttributes.Fixed, true, stateTypes);

        public static StateParameters CreateLate(params Type[] stateTypes) => new(UpdateAttributes.Late, false, stateTypes);
        public static StateParameters CreateAbortableLate(params Type[] stateTypes) => new(UpdateAttributes.Late, true, stateTypes);
    }
}
