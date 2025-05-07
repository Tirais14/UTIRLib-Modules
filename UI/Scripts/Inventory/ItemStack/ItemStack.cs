using System;
using UnityEngine;
using UTIRLib.Diagnostics;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class ItemStack : IItemStack
    {
        protected IItem? item;
        protected int quantity;

        public readonly static IItemStack Empty = new ItemStack();
        public IItem? Item => item;
        public int Quantity => quantity;
        public bool IsEmpty => item == null && quantity == 0;
        public bool IsFull => quantity >= (item?.MaxQuantity ?? 0);

        public ItemStack() { }

        public ItemStack(IItem item, int quantity = 1)
        {
            this.item = item;
            this.quantity = quantity;
        }

        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// <br/><see cref="OutOfRangeMessage"/>
        /// </remarks>
        /// <returns>Remaining items or <see cref="ItemStack"/>.Empty</returns>
        public IItemStack PutItem(IItem? item, int quantity = 1)
        {
            if (item == null || quantity < 1) {
                return Empty;
            }

            if (IsEmpty) {
                return SetItem(item, quantity);
            }
            if (!IsEmpty && Equals(item) && !IsFull) {
                return AddItem(quantity);
            }
            else {
                return ReplaceItem(item, quantity);
            }
        }

        /// <returns>Remaining items or <see cref="ItemStack"/>.Empty</returns>
        public void PutItemStack(IItemStack itemStack)
        {
            if (itemStack == null) {
                Debug.LogError(new ArgumentNullMessage(nameof(itemStack)));
                return;
            }

            int movedItemCount = itemStack.Quantity - PutItem(itemStack.Item, itemStack.Quantity).Quantity;
            if (movedItemCount > 0) {
                itemStack.TakeItem(movedItemCount);
            }
        }

        /// <returns>Item stack or <see langword="null"/></returns>
        public IItemStack TakeItem(int quantity = 1)
        {
            if (IsEmpty) {
                return Empty;
            }
            if (quantity <= 0 || quantity > item!.MaxQuantity) {
                Debug.LogError($"Quantity value cannot be {quantity}");
                return Empty;
            }

            int takedQuantity = ProccessToTakeQuantity(quantity);
            this.quantity -= takedQuantity;
            var takedItemStack = takedQuantity > 0 ? new ItemStack(item, takedQuantity) : Empty;
            UpdateInfo();
            return takedItemStack;
        }

        public bool Contains(IItem item) => !IsEmpty && this.item == item;

        /// <returns>Item stack or <see langword="null"/></returns>
        public IItemStack TakeItemAll() => TakeItem(quantity);

        protected IItemStack SetItem(IItem item, int quantity)
        {
            this.item = item;
            IItemStack remainingItemStack = CalculateRemainingItemStack(quantity);
            this.quantity = remainingItemStack.IsEmpty ? quantity : item.MaxQuantity;

            return remainingItemStack;
        }

        protected IItemStack AddItem(int quantity)
        {
            IItemStack remainingItemStack = CalculateRemainingItemStack(quantity);
            this.quantity = remainingItemStack.IsEmpty ? this.quantity + quantity : item!.MaxQuantity;

            return remainingItemStack;
        }

        protected IItemStack ReplaceItem(IItem item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;

            return TakeItem(this.quantity);
        }

        /// <returns>Actual taked item quantity</returns>
        protected int ProccessToTakeQuantity(int toTakeQuantity)
        {
            int remainingQuantity = quantity - toTakeQuantity;
            if (remainingQuantity < 0) {
                return toTakeQuantity - Math.Abs(remainingQuantity);
            }
            else return toTakeQuantity;
        }

        protected IItemStack CalculateRemainingItemStack(int quantity)
        {
            int totalQuantity = quantity + this.quantity;
            return totalQuantity - item!.MaxQuantity > 0 ? new ItemStack(item, totalQuantity - item.MaxQuantity) : Empty;
        }

        protected void UpdateInfo()
        {
            if (quantity <= 0 || item == null) {
                Reset();
            }
        }

        protected void Reset()
        {
            item = null;
            quantity = 0;
        }
    }
}
