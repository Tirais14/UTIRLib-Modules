using UnityEngine;
using UTIRLib;

#nullable enable
namespace UTIRLib.Enums
{
    public static class Direction2DExtensions
    {
        public static Vector3Int ToVector3Int(this Direction2D direction) => direction switch {
            Direction2D.Up => Vector3Int.up,
            Direction2D.Down => Vector3Int.down,
            Direction2D.Right => Vector3Int.right,
            Direction2D.Left => Vector3Int.left,
            Direction2D.RightUp => Vector3Int.right + Vector3Int.up,
            Direction2D.LeftUp => Vector3Int.left + Vector3Int.up,
            Direction2D.RightDown => Vector3Int.right + Vector3Int.down,
            Direction2D.LeftDown => Vector3Int.left + Vector3Int.down,
            _ => Vector3Int.zero
        };

        public static Vector3 ToVector3(this Direction2D direction) => ToVector3Int(direction);

        public static Vector2 ToVector2(this Direction2D direction) => ToVector3Int(direction).ToVector2Int();

        public static Vector2Int ToVector2Int(this Direction2D direction) => ToVector3Int(direction).ToVector2Int();

        public static bool IsDiagonal(this Direction2D direction) =>
            direction switch {
                Direction2D.None => false,
                Direction2D.Right => false,
                Direction2D.Left => false,
                Direction2D.Up => false,
                Direction2D.Down => false,
                Direction2D.LeftUp => true,
                Direction2D.LeftDown => true,
                Direction2D.RightUp => true,
                Direction2D.RightDown => true,
                _ => false,
            };
    }
}