using System.Collections.Generic;

#nullable enable
namespace UTIRLib
{
    public readonly struct KeyObjectPair : IKeyObjectPair
    {
        private readonly object key, value;

        public readonly object Key => key;
        public readonly object Value => value;

        public KeyObjectPair(object key, object value)
        {
            this.key = key;
            this.value = value;
        }
    }
    public readonly struct KeyObjectPair<TKey, TValue> : IKeyObjectPair<TKey, TValue>
    {
        private readonly TKey key;
        private readonly TValue value;

        public readonly TKey Key => key;
        public readonly TValue Value => value;

        public KeyObjectPair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public static implicit operator KeyValuePair<TKey, TValue>(KeyObjectPair<TKey, TValue> keyValuePair) =>
            new(keyValuePair.key, keyValuePair.value);

        object IKeyObjectPair.Key => Key!;
        object IKeyObjectPair.Value => Value!;
    }
}
