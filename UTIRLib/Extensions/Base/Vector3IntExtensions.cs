using UnityEngine;

#nullable enable
namespace UTIRLib
{
    public static class Vector3IntExtensions
    {
        public static void SetX(this Vector3Int vector3int, int x) => vector3int.Set(x, vector3int.y, vector3int.z);

        public static void SetY(this Vector3Int vector3int, int y) => vector3int.Set(vector3int.x, y, vector3int.z);

        public static void SetZ(this Vector3Int vector3int, int z) => vector3int.Set(vector3int.x, vector3int.y, z);

        public static void Set(this Vector3Int vector3int, Vector3Int value) => vector3int.Set(value.x, value.y, value.z);

        public static Vector2Int ToVector2Int(this Vector3Int vector3Int) => new(vector3Int.x, vector3Int.y);

    }
}
