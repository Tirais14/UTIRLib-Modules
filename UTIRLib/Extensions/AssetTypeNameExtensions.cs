using System;
using UnityEditor;
using UnityEngine;

#nullable enable
namespace UTIRLib.Enums
{
    public static class AssetTypeNameExtensions
    {
        public static Type? AsSytemType(this AssetTypeName assetType) =>
            assetType switch {
                AssetTypeName.None => null,
                AssetTypeName.Generic => typeof(UnityEngine.Object),
                AssetTypeName.GameObject => typeof(GameObject),
                AssetTypeName.ScriptableObject => typeof(ScriptableObject),
                AssetTypeName.Scene => typeof(SceneAsset),
                _ => null,
            };
    }
}