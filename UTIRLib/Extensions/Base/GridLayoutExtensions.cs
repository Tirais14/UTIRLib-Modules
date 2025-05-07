using UnityEngine;

namespace UTIRLib
{
    public static class GridLayoutExtensions
    {
        public static Vector2Int WorldToCellExt(this GridLayout gridLayout, Vector3 position) =>
            gridLayout.WorldToCell(position).ToVector2Int();

        public static Vector3 CellToWorldExt(this GridLayout gridLayout, Vector2Int position) =>
            gridLayout.CellToWorld(position.ToVector3Int());
    }
}
