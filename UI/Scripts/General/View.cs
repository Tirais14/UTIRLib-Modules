namespace UTIRLib.UI
{
    public abstract class View : MonoBehaviourExtended
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            BindToViewModel();
        }

        protected abstract void BindToViewModel();
    }
}
