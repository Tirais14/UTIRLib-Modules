using System.Collections.Generic;
using UnityEngine;

#nullable enable
namespace UTIRLib.Database
{
    public sealed class AssetDatabaseGroupScriptableObject : AssetDatabaseGroup<
        AssetDatabaseScriptableObject,
        AssetDatabaseItemScriptableObject,
        ScriptableObject>
    {
        public AssetDatabaseGroupScriptableObject() : base()
        { }
        public AssetDatabaseGroupScriptableObject(string databaseKey, AssetDatabaseScriptableObject database)
            : base(databaseKey, database)
        { }
        public AssetDatabaseGroupScriptableObject(IKeyObjectPair<string, AssetDatabaseScriptableObject>[] keyAndDatabases)
            : base(keyAndDatabases)
        { }
    }
}