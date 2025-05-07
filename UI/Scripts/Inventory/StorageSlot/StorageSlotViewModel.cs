using UnityEngine;
using UTIRLib.Diagnostics;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class StorageSlotViewModel : ViewModel
    {
        [SerializeField] protected StorageSlotModel storageSlotModel = null!;

        /// <remarks>Debug:
        /// <br/><see cref="ArgumentNullMessage"/>
        /// </remarks>
        public void DragItemStack(IItemStack itemStack)
        {
            if (itemStack.IsNull()) {
                Debug.LogWarning(new ArgumentNullMessage(nameof(itemStack)));
                return;
            }
            if (itemStack.IsEmpty) {
                Debug.LogError("Cannot not drag empty item stack.");
                return;
            }

            storageSlotModel.ItemStack.PutItemStack(itemStack);
        }

        protected override void BindToModel() => throw new System.NotImplementedException();
    }
}
