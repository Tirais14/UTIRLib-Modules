#nullable enable
namespace UTIRLib.UI
{
    public abstract class ViewModel : MonoBehaviourExtended
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            BindToModel();
        }

        protected abstract void BindToModel();
    }
}
