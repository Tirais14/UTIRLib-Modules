using UnityEngine;
using UTIRLib.Diagnostics;

#nullable enable
namespace UTIRLib
{
    public static class Vector3Extensions
    {
        public static void SetX(this Vector3 vector3, float x) => vector3.Set(x, vector3.y, vector3.z);

        public static void SetY(this Vector3 vector3, float y) => vector3.Set(vector3.x, y, vector3.z);

        public static void SetZ(this Vector3 vector3, float z) => vector3.Set(vector3.x, vector3.y, z);

        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static void Set(this Vector3 vector3, Transform transform)
        {
            if (transform == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(transform)));
                return;
            }

            vector3.Set(transform.position);
        }
        public static void Set(this Vector3 vector3, Vector3 value) => vector3.Set(value.x, value.y, value.z);

        public static Vector3 GetVectorDirectionRelativeTo(this Vector3 position, Vector3 target) =>
            (target - position).normalized;
    }
}