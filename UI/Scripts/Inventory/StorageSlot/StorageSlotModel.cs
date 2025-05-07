using UnityEngine;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class StorageSlotModel : MonoBehaviourExtended, IStorageSlot
    {
        protected int id = int.MinValue;
        [SerializeField] protected ItemStackModel itemStack = null!;

        public int ID => id;
        public IItemStack ItemStack => itemStack;
        public int Quantity => itemStack.Quantity;
        public bool IsEmpty => itemStack.IsEmpty;
        public bool IsFull => itemStack.IsFull;

        public void AssignID(int id)
        {
            if (IsIDAssigned()) {
                Debug.LogError($"ID value cannot be changed.");
                return;
            }

            this.id = id;
        }

        public IItemStack PutItem(IItem item, int quantity = 1) => itemStack.PutItem(item, quantity);

        public void PutItemStack(IItemStack itemStack) => itemStack.PutItemStack(itemStack);

        public IItemStack TakeItem(int quantity = 1) => itemStack.TakeItem(quantity);

        public IItemStack TakeItemAll() => itemStack.TakeItemAll();

        public bool IsIDAssigned() => id != int.MinValue;

        public bool Contains(IItem item) => ItemStack.Item == item;
    }
}
