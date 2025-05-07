namespace UTIRLib.CustomTicker
{
    public interface ILateTicker
    {
        public float TimeSpeed { get; set; }
        public int Count { get; }

        public void Register(ILateTickable lateTickable);

        public void Unregister(ILateTickable lateTickable);

        public bool Contains(ILateTickable lateTickable);

        public void UnregisterAll();
    }
    public interface ILateTicker<T>
    {
        public float TimeSpeed { get; set; }
        public int Count { get; }

        public void Register(ILateTickable<T> lateTickable);

        public void Unregister(ILateTickable<T> lateTickable);

        public bool Contains(ILateTickable<T> lateTickable);

        public void UnregisterAll();
    }
}
