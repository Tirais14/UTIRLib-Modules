namespace UTIRLib.CustomTicker
{
    public interface ILateTickable
    {
        public void OnLateTick();
    }

    public interface ILateTickable<in T>
    {
        public void OnLateTick(T parameter);
    }
}
