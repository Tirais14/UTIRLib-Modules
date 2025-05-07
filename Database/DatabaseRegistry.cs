using System;
using System.Collections;
using System.Collections.Generic;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib.Database
{
    /// <summary>
    /// Used to load and access the game's databases
    /// </summary>
    public class DatabaseRegistry<TKey> : IDatabaseRegistry<TKey>
    {
        protected readonly Dictionary<TKey, IDatabaseGroup> databaseGroups = new();

        public int Count => databaseGroups.Count;
        public IDatabaseGroup this[TKey key] => databaseGroups[key];

        public DatabaseRegistry()
        { }
        public DatabaseRegistry(TKey[] keys, IDatabaseGroup[] databaseGroups) =>
            AddGroups(keys, databaseGroups);
        public DatabaseRegistry(params IKeyObjectPair<TKey, IDatabaseGroup>[] keyDatabaseGroupPairs)
        {
            if (keyDatabaseGroupPairs.IsEmpty()) return;

            AddGroups(keyDatabaseGroupPairs);
        }

        public void AddGroup(TKey key, IDatabaseGroup databaseGroup) => databaseGroups.Add(key, databaseGroup);
        public void AddGroup(IKeyObjectPair<TKey, IDatabaseGroup> keyDatabaseGroupPair) =>
            AddGroup(keyDatabaseGroupPair.Key, keyDatabaseGroupPair.Value);

        /// <exception cref="NullOrEmptyCollectionException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddGroups(TKey[] keys, IDatabaseGroup[] databaseGroups)
        {
            if (keys.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException();
            }
            else if (databaseGroups.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException();
            }
            else if (keys.Length != databaseGroups.Length) {
                throw new ArgumentException("Array must be the same length.");
            }

            for (int i = 0; i < keys.Length; i++) {
                AddGroup(keys[i], databaseGroups[i]);
            }
        }
        /// <exception cref="NullOrEmptyCollectionException"></exception>
        public void AddGroups(IKeyObjectPair<TKey, IDatabaseGroup>[] keyDatabaseGroupPairs)
        {
            if (keyDatabaseGroupPairs.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(keyDatabaseGroupPairs, nameof(keyDatabaseGroupPairs));
            }

            for (int i = 0; i < keyDatabaseGroupPairs.Length; i++) {
                AddGroup(keyDatabaseGroupPairs[i]);
            }
        }

        public IDatabaseGroup GetDatabaseGroup(TKey key) => databaseGroups[key];
        public T? GetDatabaseGroupT<T>(TKey key) => databaseGroups[key].ConvertToType<T>();

        public IEnumerator<IDatabaseGroup> GetEnumerator() => databaseGroups.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}