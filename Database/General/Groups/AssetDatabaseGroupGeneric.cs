using System.Collections.Generic;

#nullable enable
namespace UTIRLib.Database
{
    public sealed class AssetDatabaseGroupGeneric : AssetDatabaseGroup<
        AssetDatabaseGeneric,
        AssetDatabaseItemGeneric,
        UnityEngine.Object>
    {
        public AssetDatabaseGroupGeneric() : base()
        { }
        public AssetDatabaseGroupGeneric(string databaseKey, AssetDatabaseGeneric database)
            : base(databaseKey, database)
        { }
        public AssetDatabaseGroupGeneric(IKeyObjectPair<string, AssetDatabaseGeneric>[] keyAndDatabases)
            : base(keyAndDatabases)
        { }
    }
}