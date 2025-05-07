namespace UTIRLib.UI
{
    public interface IDragInteractable
    {
    }
    public interface IDragInteractable<in T> : IDragInteractable
    {
        void OnDragged(T obj);
    }
}
