using UnityEngine;

namespace UTIRLib.UI.InventorySystem
{
    public interface IItem
    {
        public string ItemName { get; }

        public Sprite Sprite { get; }

        public int MaxQuantity { get; }
    }
}
