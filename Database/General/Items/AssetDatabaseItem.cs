using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

#nullable enable
namespace UTIRLib.Database
{
    /// <summary>
    /// Contains information about asset
    /// </summary>
    public class AssetDatabaseItem<T> : IAssetDatabaseItem<T>
    {
        protected readonly AddressableAssetInfo info;

        protected T? asset;
        protected Type? assetType;

        public AddressableAssetInfo Info => info;
        /// <summary>
        /// If not loaded - start laoding syncroniously
        /// </summary>
        public T? Asset => asset;
        public Type? AssetType => assetType;
        public bool IsAssetLoaded => ObjectHelper.IsNotNull(asset) ;
        public string AssetName => info.Name;
        public string AssetGUID => info.AssetGUID;
        public string Address => info.Address;

        public AssetDatabaseItem(AddressableAssetInfo addressableAssetInfo) => info = addressableAssetInfo;
        
        /// <exception cref="Exception"></exception>
        public async Task<T> LoadAssetAsync()
        {
            if (IsAssetLoaded) {
                Debug.LogWarning($"Asset {asset.GetTypeName()} already loaded.");
                return GetAsset()!;
            }

            asset = await Addressables.LoadAssetAsync<T>(Address).Task;
            assetType = asset!.GetType();

            return asset;
        }
        public T LoadAsset()
        {
            if (IsAssetLoaded) {
                Debug.LogWarning($"Asset {asset.GetTypeName()} already loaded.");
                return GetAsset()!;
            }

            Task<T> task = LoadAssetAsync();
            task.RunSynchronously();

            return task.Result;
        }

        public void ReleaseAsset()
        {
            if (IsAssetLoaded) {
                Addressables.Release(asset);
            }
        }

        public T? GetAsset() => asset;
    }
}