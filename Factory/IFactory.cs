namespace UTIRLib.Factory
{
#nullable enable
    public interface IFactory
    {
        public object Create(object parameter);

        public object Create(params object[] parameters);
    }
    public interface IFactory<out T>
    {
        public T Create();
    }
    public interface IFactory<in T1, out TOut>
    {
        public TOut Create(T1 paramater1);
    }
    public interface IFactory<in T1, in T2, out TOut>
    {
        public TOut Create(T1 paramater1, T2 paramater2);
    }
    public interface IFactory<in T1, in T2, in T3, out TOut>
    {

        public TOut Create(T1 paramater1, T2 paramater2, T3 parameter3);
    }
}
