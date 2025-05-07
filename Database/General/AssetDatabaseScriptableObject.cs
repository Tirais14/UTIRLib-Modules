using UnityEngine;

#nullable enable
namespace UTIRLib.Database
{
    public class AssetDatabaseScriptableObject :
        AssetDatabase<AssetDatabaseItemScriptableObject, ScriptableObject>
    {
        public AssetDatabaseScriptableObject() : base()
        { }
        public AssetDatabaseScriptableObject(bool isLogging) : base(isLogging)
        { }
    }
}