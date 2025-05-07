#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public interface IItemStack
    {
        IItem? Item { get; }
        int Quantity { get; }
        bool IsEmpty { get; }
        bool IsNotEmpty => !IsEmpty;
        bool IsFull { get; }

        IItemStack PutItem(IItem item, int quantity = 1);

        void PutItemStack(IItemStack itemStack);

        IItemStack TakeItem(int quantity = 1);

        IItemStack TakeItemAll();

        bool Contains(IItem item);
    }
}
