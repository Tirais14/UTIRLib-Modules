using System.Collections.Generic;
using UnityEngine;

#nullable enable
namespace UTIRLib.Database
{
    public sealed class AssetDatabaseGroupGameObject : AssetDatabaseGroup<
        AssetDatabaseGameObject,
        AssetDatabaseItemGameObject,
        GameObject>
    {
        public AssetDatabaseGroupGameObject() : base()
        { }
        public AssetDatabaseGroupGameObject(string databaseKey, AssetDatabaseGameObject database)
            : base(databaseKey, database)
        { }
        public AssetDatabaseGroupGameObject(IKeyObjectPair<string, AssetDatabaseGameObject>[] keyAndDatabases)
            : base(keyAndDatabases)
        { }
    }
}