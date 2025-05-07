using System.Collections.Generic;

#nullable enable
namespace UTIRLib.Database
{
    public interface IDatabaseGroup
    {
        int Count { get; }

        void AddDatabase(object databaseKey, IDatabase database);
        void AddDatabase(IKeyObjectPair<object, IDatabase> keyAndDatabasePair);

        void AddDatabaseRange(object[] databaseKeys, IDatabase[] databases);
        void AddDatabaseRange(IKeyObjectPair<object, IDatabase>[] KeyAndDatabasePairs);
    }
    public interface IDatabaseGroup<TDatabaseKey, TDatabase> : IDatabaseGroup, IReadOnlyCollection<TDatabase>
        where TDatabase : class, IDatabase
    {
        TDatabase this[TDatabaseKey databaseKey] { get; }

        void AddDatabase(TDatabaseKey databaseKey, TDatabase database);
        void AddDatabase(IKeyObjectPair<TDatabaseKey, TDatabase> keyAndDatabasePair);

        void AddDatabaseRange(TDatabaseKey[] databaseKeys, TDatabase[] databases);
        void AddDatabaseRange(IKeyObjectPair<TDatabaseKey, TDatabase>[] KeyAndDatabasePairs);

        bool Contains(TDatabaseKey databaseKey);
        bool Contains(TDatabase database);

        TDatabase GetDatabase(TDatabaseKey databaseKey);

        bool TryGetDatabase(TDatabaseKey databaseKey, out TDatabase database);
    }
}
