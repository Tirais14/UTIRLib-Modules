using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using UTIRLib.Diagnostics;

#nullable enable
namespace UTIRLib.Database
{
    public class Database<TKey, TValue> : IDatabase<TKey, TValue>
    {
        protected readonly Dictionary<TKey, TValue> db = new();
        protected readonly Func<TKey, ValidationInfo> keyValidationFunc;

        public int Count => db.Count;
        public bool IsNotEmpty => db.Count > 0;
        public TValue this[TKey key] => GetValue(key);
        public bool LogAddedValues { get; set; }

        public Database(Func<TKey, ValidationInfo>? keyValidationFunc, bool logAddedValues)
        {
            this.keyValidationFunc = keyValidationFunc ?? DefaultKeyValidationFunc;
            LogAddedValues = logAddedValues;
        }
        public Database(Func<TKey, ValidationInfo>? keyValidationFunc) : this(keyValidationFunc, false)
        { }
        public Database(bool logAddedValues) : this(keyValidationFunc: null, logAddedValues)
        { }
        public Database() : this(keyValidationFunc: null, logAddedValues: false)
        { }

        /// <exception cref="ArgumentException"></exception>
        public void AddValue(IKeyObjectPair value)
        {
            if (value.Key is TKey typedKey && value.Value is TValue typedValue) {
                AddValue(new KeyObjectPair<TKey, TValue>(typedKey, typedValue));
            }
            else {
                throw new ArgumentException($"Incorrect key value pair. Key = {value.Key.GetTypeName()}, " +
                    $"expected = {typeof(TKey).Name}. Value = {value.Value.GetTypeName()}, " +
                    $"expected = {typeof(TValue).Name}");
            }
        }
        public void AddValue(IKeyObjectPair<TKey, TValue> value)
        {
            if (value == null) {
                throw new ArgumentNullException(nameof(value));
            }

            db.Add(value.Key, value.Value);
            if (LogAddedValues) {
                LogAddedValue(value);
            }
        }

        public void AddValues(IKeyObjectPair[] values)
        {
            for (int i = 0; i < values.Length; i++) {
                AddValue(values[i]);
            }
        }
        public void AddValues(IKeyObjectPair<TKey, TValue>[] values)
        {
            for (int i = 0; i < values.Length; i++) {
                AddValue(values[i]);
            }
        }

        public bool Contains(TKey? key) => key.IsNotDefault() && db.ContainsKey(key);
        public bool Contains(TValue? value) => value.IsNotDefault() && db.ContainsValue(value);

        /// <exception cref="NotFoundException"></exception>
        public TValue GetValue(TKey key) => keyValidationFunc(key).HasException(out Exception exception) ?
            throw exception : db[key];
        /// <exception cref="ArgumentNullException"></exception>
        public TValue? GetValue(Type type, TKey key)
        {
            if (type == null) {
                throw new ArgumentNullException(nameof(type));
            }

            TValue value = GetValue(key);
            if (type.IsInstanceOfType(value)) {
                return value;
            }
            else return default;
        }
        public T? GetValueT<T>(TKey key)
        {
            TValue value = GetValue(key);
            if (value is T typedValue) {
                return typedValue;
            }
            else return default;
        }

        public bool TryGetValue(TKey key, [NotNullWhen(true)] out TValue? value)
        {
            value = GetValue(key);

            return value.IsNotDefault();
        }
        public bool TryGetValue(Type type, TKey key, [NotNullWhen(true)] out TValue? value)
        {
            value = GetValue(type, key);

            return value.IsNotDefault();
        }
        public bool TryGetValueT<T>(TKey key, [NotNullWhen(true)] out T? value)
        {
            value = GetValueT<T>(key);

            return value.IsNotDefault();
        }

        public TValue? FindValueByKey(Predicate<TKey> keyFilter)
        {
            DelegateValidation(keyFilter).TryThrow();

            return FindValueInternal(keyFilter, objectFilter: null);
        }
        public TValue? FindValue(Predicate<TValue> objectFilter)
        {
            DelegateValidation(objectFilter).TryThrow();

            return FindValueInternal(keyFilter: null, objectFilter);
        }
        public TValue? FindValue(Predicate<TKey> keyFilter, Predicate<TValue> objectFilter)
        {
            DelegateValidation(keyFilter, objectFilter).TryThrow();

            return FindValueInternal(keyFilter, objectFilter);
        }

        public TValue? FindValue(Type type) => FindValue((obj) => TypeFilter(obj, type));
        public T? FindValueT<T>() => FindValue((obj) => obj is T).ConvertToType<T>();

        public bool TryFindValueT<T>([NotNullWhen(true)] out T? value)
        {
            value = FindValueT<T>();

            return value.IsNotDefault();
        }
        public bool TryFindValue(Type type, [NotNullWhen(true)] out TValue? value)
        {
            value = FindValue(type);

            return value.IsNotDefault();
        }
        public bool TryFindValue(Predicate<TValue> objectFilter, [NotNullWhen(true)] out TValue? value)
        {
            value = FindValue(objectFilter);

            return value.IsNotDefault();
        }
        public bool TryFindValue(Predicate<TKey> keyFilter, Predicate<TValue> objectFilter,
            [NotNullWhen(true)] out TValue? value)
        {
            value = FindValueByKey(keyFilter);

            return value.IsNotDefault();
        }

        public bool TryFindValueByKey(Predicate<TKey> keyFilter, [NotNullWhen(true)] out TValue? value)
        {
            value = FindValueByKey(keyFilter);

            return value.IsNotDefault();
        }

        public TValue[] FindValues(Type type) => FindValues((obj) => TypeFilter(obj, type));
        public TValue[] FindValues(Predicate<TValue> objectFilter)
        {
            DelegateValidation(objectFilter).TryThrow();

            return WhereInternal(keyFilter: null, objectFilter);
        }
        public TValue[] FindValues(Predicate<TKey> keyFilter, Predicate<TValue> objectFilter)
        {
            DelegateValidation(keyFilter, objectFilter).TryThrow();

            return WhereInternal(keyFilter, objectFilter);
        }

        public TValue[] FindValuesByKey(Predicate<TKey> keyFilter)
        {
            DelegateValidation(keyFilter).TryThrow();

            return WhereInternal(keyFilter, objectFilter: null);
        }

        public T[] FindValuesT<T>() => FindValues((obj) => obj is T).ConvertToType<T[]>() ?? Array.Empty<T>();

        public bool TryFindValues(Predicate<TValue> objectFilter, [NotNullWhen(true)] out TValue[] values)
        {
            values = FindValues(objectFilter);

            return values.IsNotDefault();
        }
        public bool TryFindValues(Type type, out TValue[] values)
        {
            values = FindValues(type);

            return values.Length > 0;
        }
        public bool TryFindValues(Predicate<TKey> keyFilter, Predicate<TValue> objectFilter, 
            [NotNullWhen(true)] out TValue[] values)
        {
            values = FindValues(keyFilter, objectFilter);

            return values.IsNotDefault();
        }
        public bool TryFindValuesT<T>(out T[] values)
        {
            values = FindValuesT<T>();

            return values.Length > 0;
        }

        public bool TryFindValuesByKey(Predicate<TKey> keyFilter, [NotNullWhen(true)] out TValue[] values)
        {
            values = FindValuesByKey(keyFilter);

            return values.IsNotDefault();
        }

        public IEnumerator<TValue> GetEnumerator() => db.Values.GetEnumerator();

        private TValue? FindValueInternal(Predicate<TKey>? keyFilter, Predicate<TValue>? objectFilter) =>
            FindWithFilters(keyFilter, objectFilter).FirstOrDefault();

        private TValue[] WhereInternal(Predicate<TKey>? keyFilter, Predicate<TValue>? objectFilter) => 
            FindWithFilters(keyFilter, objectFilter);

        protected static void LogAddedValue(TKey key, TValue value) =>
            Debug.Log($"Asset registered: {key} ({value.GetTypeName()})");
        protected static void LogAddedValue(IKeyObjectPair<TKey, TValue> value) =>
            LogAddedValue(value.Key, value.Value);

        protected static ValidationInfo DelegateValidation(params Delegate[] filters)
        {
            for (int i = 0; i < filters.Length; i++) {
                if (filters[i] is null) {
                    return new ValidationInfo(new NullReferenceException("Object filter not setted."));
                }
            }

            return default;
        }

        protected static bool TypeFilter(object? value, Type type) => value != null &&
            type.IsInstanceOfType(value);

        private TValue[] FindWithFilters(Predicate<TKey>? keyFilter, Predicate<TValue>? objectFilter,
            bool onlyFirst = false)
        {
            List<TValue> foundObjects = new();
            if (keyFilter is not null) {
                foreach (KeyValuePair<TKey, TValue> dbValuePair in db) {
                    if (keyFilter(dbValuePair.Key)) {
                        foundObjects.Add(dbValuePair.Value);
                        if (onlyFirst) {
                            break;
                        }
                    }
                }
            }
            else if (objectFilter is not null) {
                foreach (TValue value in db.Values) {
                    if (objectFilter(value)) {
                        foundObjects.Add(value);
                        if (onlyFirst) {
                            break;
                        }
                    }
                }
            }
            else if (keyFilter is not null && objectFilter is not null) {
                foreach (KeyValuePair<TKey, TValue> dbValuePair in db) {
                    if (keyFilter(dbValuePair.Key) && objectFilter(dbValuePair.Value)) {
                        foundObjects.Add(dbValuePair.Value);
                        if (onlyFirst) {
                            break;
                        }
                    }
                }
            }

            return foundObjects.ToArray();
        }

        private static ValidationInfo DefaultKeyValidationFunc(TKey key)
        {
            if (key?.Equals(default) ?? true) {
                return new ValidationInfo(new ArgumentException(nameof(key)));
            }

            return default;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}