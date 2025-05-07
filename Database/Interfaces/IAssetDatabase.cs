using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceLocations;
using static UnityEngine.AddressableAssets.Addressables;
using Object = UnityEngine.Object;

#nullable enable
namespace UTIRLib.Database
{
    public interface IAssetDatabase : IDatabase
    {
        bool IsAssetLoaded(string assetName);

        string GetAssetGUID(string assetName);

        string GetAssetAddress(string assetName);

        void ReleaseAsset(string assetName);

        void ReleaseAllAssets();

        bool TryReleaseAsset(string assetName);

        bool TryReleaseAllAssets(string assetName);

        Task LoadAllAssetsAsync();
    }
    public interface IAssetDatabase<TDatabaseItem, TAsset> : IAssetDatabase, IDatabase<string, TDatabaseItem>
        where TDatabaseItem : IAssetDatabaseItem<TAsset>
    {
        new TAsset? this[string assetName] { get; }

        Task<TAsset> LoadAssetAsync(string assetName);

        Task<IList<TAsset>> LoadAssetsAsync(IList<IResourceLocation> locations, Action<TAsset>? callback = null,
            bool releaseDependenciesOnFailure = false);
        Task<IList<TAsset>> LoadAssetsAsync(string[] keys, MergeMode mergeMode = MergeMode.Union,
            Action<TAsset>? callback = null, bool releaseDependenciesOnFailure = false);

        new Task<IList<TAsset>> LoadAllAssetsAsync();

        TAsset? GetAsset(string assetName);
        TAsset? GetAsset(Type assetType, string assetName);

        T? GetAssetT<T>(string assetName) where T : Object;

        bool TryGetAsset(string assetName, [NotNullWhen(true)] out TAsset? asset);
        bool TryGetAsset(Type assetType, string assetName, [NotNullWhen(true)] out TAsset? asset);

        bool TryGetAssetT<T>(string assetName, [NotNullWhen(true)] out T? asset) where T : Object;

        Task<TAsset> GetOrLoadAssetAsync(string assetName);

        TAsset? FindAsset(string assetNamePart);
        TAsset? FindAsset(Type assetType);
        TAsset? FindAsset(string assetNamePart, Type assetType);

        T? FindAssetT<T>() where T : Object;
        T? FindAssetT<T>(string assetNamePart) where T : Object;

        bool TryFindAsset(string assetNamePart, [NotNullWhen(true)] out TAsset? asset);
        bool TryFindAsset(Type assetType, [NotNullWhen(true)] out TAsset? asset);
        bool TryFindAsset(string assetNamePart, Type assetType, [NotNullWhen(true)] out TAsset? asset);

        bool TryFindAssetT<T>(string assetNamePart, [NotNullWhen(true)] out T? asset) where T : Object;
        bool TryFindAssetT<T>([NotNullWhen(true)] out T? asset) where T : Object;

        TAsset[] FindAssets(string assetNamePart);
        TAsset[] FindAssets(Type assetType);
        TAsset[] FindAssets(string assetNamePart, Type assetType);

        bool TryFindAssets(string assetNamePart, out TAsset[] assets);
        bool TryFindAssets(Type assetType, out TAsset[] assets);
        bool TryFindAssets(string assetNamePart, Type assetType, out TAsset[] assets);

        T[] FindAssetsT<T>() where T : Object;
        T[] FindAssetsT<T>(string assetNamePart) where T : Object;

        bool TryFindAssetsT<T>(string assetNamePart, out T[] asset) where T : Object;
        bool TryFindAssetsT<T>(out T[] assets) where T : Object;
    }
}
