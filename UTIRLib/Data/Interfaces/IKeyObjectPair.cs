#nullable enable
namespace UTIRLib
{
    public interface IKeyObjectPair
    {
        object Key { get; }
        object Value { get; }
    }
    public interface IKeyObjectPair<out Tkey, out TValue> : IKeyObjectPair
    {
        new Tkey Key { get; }
        new TValue Value { get; }
    }
}
