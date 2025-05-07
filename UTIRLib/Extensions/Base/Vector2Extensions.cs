using UnityEngine;
using UTIRLib.Diagnostics;
using UTIRLib.Enums;

#nullable enable
namespace UTIRLib
{
    public static class Vector2Extensions
    {
        public static void SetX(this Vector2 vector2, float x) => vector2.Set(x, vector2.y);

        public static void SetY(this Vector2 vector2, float y) => vector2.Set(vector2.x, y);

        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public static void Set(this Vector2 vector2, Transform transform)
        {
            if (transform == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(transform)));
                return;
            }

            vector2.Set(transform.position);
        }
        public static void Set(this Vector2 vector2, Vector2 value) => vector2.Set(value.x, value.y);

        public static Vector3 ToVector3(this Vector2 vector2) => new(vector2.x, vector2.y);

        public static Direction2D ToDirectionEnum(this Vector2 movementVector)
        {
            if (!movementVector.x.EqualsAround(0) && !movementVector.y.EqualsAround(0)) {
                if (movementVector.x > 0 && movementVector.y > 0) {
                    return Direction2D.RightUp;
                }
                else if (movementVector.x < 0 && movementVector.y > 0) {
                    return Direction2D.LeftUp;
                }
                else if (movementVector.x > 0 && movementVector.y < 0) {
                    return Direction2D.RightDown;
                }
                else if (movementVector.x < 0 && movementVector.y < 0) {
                    return Direction2D.LeftDown;
                }
            }
            else if (!movementVector.x.EqualsAround(0) && movementVector.y.EqualsAround(0)) {
                if (movementVector.x > 0) {
                    return Direction2D.Right;
                }
                else if (movementVector.x < 0) {
                    return Direction2D.Left;
                }
            }
            else if (!movementVector.y.EqualsAround(0) && movementVector.x.EqualsAround(0)) {
                if (movementVector.y > 0) {
                    return Direction2D.Up;
                }
                else if (movementVector.y < 0) {
                    return Direction2D.Down;
                }
            }

            return Direction2D.None;
        }
    }
}