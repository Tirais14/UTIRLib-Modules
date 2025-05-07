using System;
using System.Collections;
using System.Collections.Generic;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib.Database
{
    public class DatabaseGroup<TDatabaseKey, TDatabase> : IDatabaseGroup<TDatabaseKey, TDatabase>
        where TDatabase : class, IDatabase
    {
        protected readonly Dictionary<TDatabaseKey, TDatabase> dbs = new();

        public int Count => dbs.Count;
        public TDatabase this[TDatabaseKey databaseKey] => GetDatabase(databaseKey);

        public void AddDatabase(object databaseKey, IDatabase database) =>
            dbs.Add(databaseKey.ConvertToType<TDatabaseKey>() ?? throw new NullReferenceException(),
            database as TDatabase ?? throw new NullReferenceException());
        public void AddDatabase(IKeyObjectPair<object, IDatabase> keyAndDatabasePair) =>
            AddDatabase(keyAndDatabasePair.Key, keyAndDatabasePair.Value);
        public void AddDatabase(TDatabaseKey databaseKey, TDatabase database) => dbs.Add(databaseKey, database);
        public void AddDatabase(IKeyObjectPair<TDatabaseKey, TDatabase> keyAndDatabasePair) =>
            AddDatabase(keyAndDatabasePair.Key, keyAndDatabasePair.Value);

        public void AddDatabaseRange(object[] databaseKeys, IDatabase[] databases)
        {
            if (databaseKeys.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(databaseKeys, nameof(databaseKeys));
            }
            else if (databases.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(databases, nameof(databases));
            }
            else if (databaseKeys.Length != databases.Length) {
                throw new ArgumentException("Key and database arrays must be the same length.");
            }

            for (int i = 0; i < databaseKeys.Length; i++) {
                AddDatabase(databaseKeys[i], databases[i]);
            }
        }
        public void AddDatabaseRange(IKeyObjectPair<object, IDatabase>[] KeyAndDatabasePairs)
        {
            if (KeyAndDatabasePairs.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(KeyAndDatabasePairs, nameof(KeyAndDatabasePairs));
            }

            for (int i = 0; i < KeyAndDatabasePairs.Length; i++) {
                AddDatabase(KeyAndDatabasePairs[i]);
            }
        }
        public void AddDatabaseRange(TDatabaseKey[] databaseKeys, TDatabase[] databases)
        {
            if (databaseKeys.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(databaseKeys, nameof(databaseKeys));
            }
            else if (databases.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(databases, nameof(databases));
            }
            else if (databaseKeys.Length != databases.Length) {
                throw new ArgumentException("Key and database arrays must be the same length.");
            }

            for (int i = 0; i < databaseKeys.Length; i++) {
                AddDatabase(databaseKeys[i], databases[i]);
            }
        }
        public void AddDatabaseRange(IKeyObjectPair<TDatabaseKey, TDatabase>[] KeyAndDatabasePairs)
        {
            if (KeyAndDatabasePairs.IsNullOrEmpty()) {
                throw new NullOrEmptyCollectionException(KeyAndDatabasePairs, nameof(KeyAndDatabasePairs));
            }

            for (int i = 0; i < KeyAndDatabasePairs.Length; i++) {
                AddDatabase(KeyAndDatabasePairs[i]);
            }
        }

        public TDatabase GetDatabase(TDatabaseKey databaseKey) => dbs[databaseKey];

        public bool TryGetDatabase(TDatabaseKey databaseKey, out TDatabase database) => dbs.TryGetValue(databaseKey, out database);

        public bool Contains(TDatabaseKey databaseKey) => dbs.ContainsKey(databaseKey);
        public bool Contains(TDatabase database) => dbs.ContainsValue(database);

        public IEnumerator<TDatabase> GetEnumerator() => dbs.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}