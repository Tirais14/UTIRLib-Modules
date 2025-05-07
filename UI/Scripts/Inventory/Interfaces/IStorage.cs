using System.Collections.Generic;

namespace UTIRLib.UI.InventorySystem
{
    public interface IStorage : IReadOnlyList<IStorageSlot>, IClosable
    {
        int SlotQuantity { get; set; }

        IItemStack AddItem(IItem item, int quantity);

        void AddItemStack(IItemStack itemStack);

        void SetSlotQuantity(int quantity);

        IStorageSlot GetSlot(int id);

        IStorageSlot GetSuitableSlot(IItem item);
    }
}
