#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Object = UnityEngine.Object;

namespace UTIRLib.Database
{
    public interface IAssetDatabaseGroup : IDatabaseGroup
    {
    }
    public interface IAssetDatabaseGroup<TDatabase, TDatabaseItem, TAsset> : IAssetDatabaseGroup, 
        IDatabaseGroup<string, TDatabase>
        where TDatabaseItem : IAssetDatabaseItem<TAsset>
        where TDatabase : class, IDatabase<string, TDatabaseItem>
    {
        TAsset? GetAsset(string databaseName, string assetName);
        TAsset? GetAsset(string databaseName, Type assetType, string assetName);

        T? GetAssetT<T>(string databaseName, string assetName) where T : Object;

        bool TryGetAsset(string databaseName, string assetName, [NotNullWhen(true)] out TAsset? asset);
        bool TryGetAsset(string databaseName, Type assetType, string assetName, [NotNullWhen(true)] out TAsset? asset);

        bool TryGetAssetT<T>(string databaseName, string assetName, [NotNullWhen(true)] out T? asset) where T : Object;

        TAsset? FindAsset(string assetNamePart, Type assetType);
        TAsset? FindAsset(string assetNamePart);
        TAsset? FindAsset(Type assetType);

        T? FindAssetT<T>(string assetNamePart) where T : Object;
        T? FindAssetT<T>() where T : Object;

        bool TryFindAsset(string assetNamePart, Type assetType, [NotNullWhen(true)] out TAsset? asset);
        bool TryFindAsset(string assetNamePart, [NotNullWhen(true)] out TAsset? asset);
        bool TryFindAsset(Type assetType, [NotNullWhen(true)] out TAsset? asset);

        bool TryFindAssetT<T>(string assetNamePart, [NotNullWhen(true)] out T? asset) where T : Object;
        bool TryFindAssetT<T>([NotNullWhen(true)] out T? asset) where T : Object;

        TAsset[] FindAssets(string assetNamePart, Type assetType);
        TAsset[] FindAssets(string assetNamePart);
        TAsset[] FindAssets(Type assetType);

        T[] FindAssetsT<T>(string assetNamePart) where T : Object;
        T[] FindAssetsT<T>() where T : Object;

        bool TryFindAssets(string assetNamePart, Type assetType, out TAsset[] assets);
        bool TryFindAssets(string assetNamePart, out TAsset[] assets);
        bool TryFindAssets(Type assetType, out TAsset[] assets);

        bool TryFindAssetsT<T>(string assetNamePart, out T[] assets) where T : Object;
        bool TryFindAssetsT<T>(out T[] assets) where T : Object;
    }
}
