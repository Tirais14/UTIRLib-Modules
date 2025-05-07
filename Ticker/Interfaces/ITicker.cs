namespace UTIRLib.CustomTicker
{
    public interface ITicker
    {
        public float TimeSpeed { get; set; }
        public int Count { get; }

        public void Register(ITickable tickable);

        public void Unregister(ITickable tickable);

        public bool Contains(ITickable tickable);

        public void UnregisterAll();

        public void SetTimeSpeed(float value);

        public void ResetTimeSpeed();
    }
    public interface ITicker<T>
    {
        public float TimeSpeed { get; set; }
        public int Count { get; }

        public void Register(ITickable<T> tickable);

        public void Unregister(ITickable<T> tickable);

        public bool Contains(ITickable<T> tickable);

        public void UnregisterAll();

        public void SetTimeSpeed(float value);

        public void ResetTimeSpeed();
    }
}
