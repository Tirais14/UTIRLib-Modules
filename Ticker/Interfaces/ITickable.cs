namespace UTIRLib.CustomTicker
{
    public interface ITickable
    {
        public void OnTick();

    }
    public interface ITickable<in T>
    {
        public void OnTick(T parameter);
    }
}
