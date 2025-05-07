using UnityEngine;

#nullable enable
namespace UTIRLib
{
    public static class Vector2IntExtensions
    {
        public static void SetX(this Vector2Int vector2Int, int x) => vector2Int.Set(x, vector2Int.y);

        public static void SetY(this Vector2Int vector2Int, int y) => vector2Int.Set(vector2Int.x, y);

        public static void Set(this Vector2Int vector2Int, Vector2Int value) => vector2Int.Set(value.x, value.y);

        public static Vector3Int ToVector3Int(this Vector2Int vector2Int) => new(vector2Int.x, vector2Int.y);
    }
}
