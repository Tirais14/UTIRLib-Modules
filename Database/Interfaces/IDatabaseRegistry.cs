#nullable enable
using System.Collections.Generic;

namespace UTIRLib.Database
{
    public interface IDatabaseRegistry : IReadOnlyCollection<IDatabaseGroup>
    {
    }
    public interface IDatabaseRegistry<in TKey> : IDatabaseRegistry
    {
        IDatabaseGroup this[TKey key] { get; }

        void AddGroup(TKey key, IDatabaseGroup databaseGroup);
        void AddGroup(IKeyObjectPair<TKey, IDatabaseGroup> keyDatabaseGroupPair);

        void AddGroups(TKey[] keys, IDatabaseGroup[] databaseGroups);
        void AddGroups(IKeyObjectPair<TKey, IDatabaseGroup>[] keyDatabaseGroupPairs);

        IDatabaseGroup GetDatabaseGroup(TKey key);
        T? GetDatabaseGroupT<T>(TKey key);
    }
}
