using UnityEngine;

#nullable enable
namespace UTIRLib.Database
{
    public class AssetDatabaseItemScriptableObject : AssetDatabaseItem<ScriptableObject>
    {
        public AssetDatabaseItemScriptableObject(AddressableAssetInfo addressableAssetInfo) :
            base(addressableAssetInfo)
        { }
    }
}