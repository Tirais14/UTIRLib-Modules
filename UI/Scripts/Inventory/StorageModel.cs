using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class StorageModel : MonoBehaviourExtended, IStorage
    {
        protected readonly ReactiveCollection<StorageSlotModel> storageSlotsProp = new();
        protected readonly ReactiveProperty<bool> isOpenedProp = new();
        [SerializeField] protected GameObject slotPrefab = null!;

        public IReadOnlyReactiveCollection<StorageSlotModel> StorageSlotsProp => storageSlotsProp;
        public IReadOnlyReactiveProperty<bool> IsOpenedProp => isOpenedProp;
        public bool Opened => isOpenedProp.Value;
        public int SlotQuantity {
            get => storageSlotsProp.Count;
            set => SetSlotQuantity(value);
        }

        public int Count => SlotQuantity;
        public IStorageSlot this[int index] => GetSlot(index);

        public void Open() => isOpenedProp.Value = true;

        public void Close() => isOpenedProp.Value = false;

        public IItemStack AddItem(IItem item, int quantity)
        {
            if (item.IsNull()) {
                throw new ArgumentNullException(nameof(item));
            }
            if (quantity < 1) {
                return ItemStack.Empty;
            }

            const int CycleLimiter = 1000;
            int safetyCounter = 0;
            IStorageSlot? slot;
            IItemStack? remainingItemStack = null;
            do {
                slot = GetSuitableSlot(item);
                slot = slot.IsNotNull() ? slot : GetEmptySlot();
                if (slot.IsNotNull()) {
                    remainingItemStack = slot.PutItem(item, quantity);
                    if (remainingItemStack.IsEmpty) {
                        break;
                    }
                }
                else if (slot.IsNull() && remainingItemStack.IsNotNull() && !remainingItemStack.IsEmpty) {
                    return remainingItemStack;
                }
                safetyCounter++;
            }
            while (slot != null && safetyCounter < CycleLimiter);

            if (safetyCounter > CycleLimiter) {
                Debug.LogError("Endless loop prevented.");
            }

            return ItemStack.Empty;
        }

        /// <exception cref="ArgumentNullException"></exception>
        public void AddItemStack(IItemStack itemStack)
        {
            if (itemStack.IsNull()) {
                throw new ArgumentNullException(nameof(itemStack));
            }
            if (itemStack.IsEmpty) {
                return;
            }

            IItemStack temp = itemStack.TakeItemAll();
            AddItem(temp.Item!, temp.Quantity);
            if (itemStack.IsNotNull() && !itemStack.IsEmpty) {
                itemStack.PutItemStack(itemStack);
            }
        }

        public IStorageSlot GetSlot(int id) => storageSlotsProp[id];

        public IStorageSlot? GetEmptySlot() => SearchForEmptySlot();

        /// <exception cref="ArgumentNullException"></exception>
        public IStorageSlot? GetSuitableSlot(IItem item)
        {
            if (item.IsNull()) {
                throw new ArgumentNullException(nameof(item));
            }

            IStorageSlot? result = SearchForSuitableSlot(item);

            return result;
        }

        public void SetSlotQuantity(int quantity)
        {
            quantity = quantity < 0 ? 0 : quantity;

            if (quantity > storageSlotsProp.Count) {
                AddSlots(quantity - storageSlotsProp.Count);
            }
            else if (quantity < storageSlotsProp.Count) {
                RemoveSlots(storageSlotsProp.Count - quantity);
            }
        }

        public IEnumerator<IStorageSlot> GetEnumerator() => storageSlotsProp.GetEnumerator();

        protected IStorageSlot? SearchForEmptySlot()
        {
            for (int i = 0; i < storageSlotsProp.Count; i++) {
                if (storageSlotsProp[i].IsEmpty) {
                    return storageSlotsProp[i];
                }
            }

            return null;
        }

        protected IStorageSlot? SearchForSuitableSlot(IItem item)
        {
            IStorageSlot slot;
            for (int i = 0; i < storageSlotsProp.Count; i++) {
                slot = storageSlotsProp[i];
                if (!slot.IsEmpty && slot.Contains(item) && !slot.IsFull) {
                    return slot;
                }
            }

            return null;
        }

        protected StorageSlotModel AddSlot() => Instantiate(slotPrefab, transform).GetComponent<StorageSlotModel>();

        protected void AddSlots(int toCreateSlotQuantity)
        {
            StorageSlotModel storageSlot;
            int storageSlotsBeforeCount = storageSlotsProp.Count;
            for (int i = 0; i < toCreateSlotQuantity; i++) {
                storageSlot = AddSlot();
                storageSlot.AssignID(storageSlotsBeforeCount + i);
                storageSlotsProp.Add(storageSlot);
            }
        }

        protected void RemoveSlot()
        {
            if (storageSlotsProp[^1] is MonoBehaviour unityObj) {
                Destroy(unityObj.gameObject);
            }

            storageSlotsProp.Remove(storageSlotsProp[^1]);
        }

        protected void RemoveSlots(int quantity)
        {
            for (int i = 0; i < quantity; i++) {
                RemoveSlot();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
