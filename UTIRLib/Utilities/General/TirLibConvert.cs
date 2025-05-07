using System;
using UnityEditor;
using UnityEngine;
using UTIRLib.Diagnostics;
using UTIRLib.Enums;

#nullable enable
namespace UTIRLib
{
    public static partial class TirLibConvert
    {
        public static AssetTypeName SystemTypeToAssetType(Type assetObjectType)
        {
            if (assetObjectType == null) { Debug.LogError(new ArgumentNullMessage(nameof(assetObjectType))); return AssetTypeName.None; }

            if (assetObjectType == typeof(GameObject) || assetObjectType.IsSubclassOf(typeof(GameObject))) { return AssetTypeName.GameObject; }
            else if (assetObjectType == typeof(ScriptableObject) ||
                assetObjectType.IsSubclassOf(typeof(ScriptableObject))) { return AssetTypeName.ScriptableObject; }
            else if (assetObjectType == typeof(SceneAsset) ||
                assetObjectType.IsSubclassOf(typeof(SceneAsset))) { return AssetTypeName.Scene; }
            else { return AssetTypeName.Generic; }
        }

        public static Direction2D ToDirection(Vector2 vector2)
        {
            float x = vector2.normalized.x;
            float y = vector2.normalized.y;
            if (x.NotEqualsAround(0) && y.NotEqualsAround(0)) {
                if (x > 0 && y > 0) { return Direction2D.RightUp; }
                else if (x < 0 && y > 0) { return Direction2D.LeftUp; }
                else if (x > 0 && y < 0) { return Direction2D.RightDown; }
                else if (x < 0 && y < 0) { return Direction2D.LeftDown; }
            }
            else if (x.NotEqualsAround(0) && y.EqualsAround(0)) {
                if (x > 0) { return Direction2D.Right; }
                else if (x < 0) { return Direction2D.Left; }
            }
            else if (x.EqualsAround(0) && y.NotEqualsAround(0)) {
                if (y > 0) { return Direction2D.Up; }
                else if (y < 0) { return Direction2D.Down; }
            }

            return Direction2D.None;
        }
        public static Direction2D ToDirection(Vector2Int vector2Int) =>
            ToDirection(new Vector2(vector2Int.x, vector2Int.y));
    }
}