using UnityEngine;

#nullable enable
namespace UTIRLib.Database
{
    /// <summary>
    /// Contains information about Game Object asset
    /// </summary>
    public class AssetDatabaseItemGameObject : AssetDatabaseItem<GameObject>
    {
        public AssetDatabaseItemGameObject(AddressableAssetInfo addressableAssetInfo) :
            base(addressableAssetInfo)
        { }
    }
}