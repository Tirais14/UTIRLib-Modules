using System;

#nullable enable
namespace UTIRLib.Database
{
    public class AssetDatabaseGeneric : AssetDatabase<AssetDatabaseItemGeneric, UnityEngine.Object>
    {
        public AssetDatabaseGeneric() : base()
        { }
        public AssetDatabaseGeneric(bool isLogging) : base(isLogging)
        { }
    }
}