using UTIRLib;
using UTIRLib.AnimatorSystem;

namespace Unnamed2DTopDownGame
{
    public class CompositeAnimatorState : Composite<IAnimatorState>, IComposite<IAnimatorState>
    {
        public CompositeAnimatorState(params IAnimatorState[] states) : base(states)
        { }
    }
}
