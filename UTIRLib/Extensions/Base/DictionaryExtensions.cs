using System.Collections.Generic;

#nullable enable
namespace UTIRLib
{
    public static class DictionaryExtensions
    {
        public static bool NotContainsKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) =>
            !dictionary.ContainsKey(key);

        public static bool NotContainsValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value) =>
            !dictionary.ContainsValue(value);
    }
}
