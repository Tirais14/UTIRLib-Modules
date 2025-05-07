namespace UTIRLib.CustomTicker
{
    public interface IFixedTickable
    {
        public void OnFixedTick();
    }

    public interface IFixedTickable<in T>
    {
        public void OnFixedTick(T parameter);
    }
}
