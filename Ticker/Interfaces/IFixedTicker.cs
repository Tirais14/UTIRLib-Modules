namespace UTIRLib.CustomTicker
{
    public interface IFixedTicker
    {
        public float TimeSpeed { get; set; }
        public int Count { get; }

        public void Register(IFixedTickable fixedTickable);

        public void Unregister(IFixedTickable fixedTickable);

        public bool Contains(IFixedTickable fixedTickable);

        public void UnregisterAll();
    }
    public interface IFixedTicker<T>
    {
        public float TimeSpeed { get; set; }
        public int Count { get; }

        public void Register(IFixedTickable<T> fixedTickable);

        public void Unregister(IFixedTickable<T> fixedTickable);

        public bool Contains(IFixedTickable<T> fixedTickable);

        public void Clear();
    }
}
