using UniRx;
using UnityEngine;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class ItemStackModel : MonoBehaviourExtended, IItemStack
    {
        protected readonly ItemStack baseItemStack = new();
        protected readonly ReactiveProperty<IItem?> itemProp = new();
        protected readonly ReactiveProperty<int> quantityProp = new();

        public IReadOnlyReactiveProperty<IItem?> ItemProp => itemProp;
        public IReadOnlyReactiveProperty<int> QuantityProp => quantityProp;
        public IItem? Item => baseItemStack.Item;
        public int Quantity => baseItemStack.Quantity;
        public bool IsEmpty => baseItemStack.IsEmpty;
        public bool IsFull => baseItemStack.IsFull;

        public IItemStack PutItem(IItem item, int quantity)
        {
            IItemStack remainingItems = baseItemStack.PutItem(item, quantity);
            UpdateInfo();
            return remainingItems;
        }

        public void PutItemStack(IItemStack itemStack)
        {
            baseItemStack.PutItemStack(itemStack);
            UpdateInfo();
        }

        public IItemStack TakeItem(int quantity)
        {
            IItemStack remainingItems = baseItemStack.TakeItem(quantity);
            UpdateInfo();
            return remainingItems;
        }

        public bool Contains(IItem item) => baseItemStack.Contains(item);

        public IItemStack TakeItemAll() => baseItemStack.TakeItemAll();

        protected void UpdateInfo()
        {
            UpdateReactiveItem();
            UpdateReactiveCount();
        }

        protected void UpdateReactiveItem() => itemProp.Value = baseItemStack.Item;

        protected void UpdateReactiveCount() => quantityProp.Value = baseItemStack.Quantity;
    }
}
