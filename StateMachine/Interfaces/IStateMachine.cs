#nullable enable
namespace UTIRLib.StateMachineSystem
{
    public interface IStateMachine : IReadOnlyStateMachine
    {
        void AddTransition(StateTransition transition);

        void AddTransitions(params StateTransition[] transitions);

        void AddState(IReadOnlyState state);

        void AddStates(params IReadOnlyState[] states);

        void RemoveState(IReadOnlyState? state);

        void RemoveStates(params IReadOnlyState?[] states);

        void RemoveTransition(StateTransition transition);

        void RemoveTransitions(params StateTransition[] transitions);
    }
}
