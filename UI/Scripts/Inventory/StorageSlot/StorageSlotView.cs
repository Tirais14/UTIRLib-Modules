using UnityEngine;
using UnityEngine.UI;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    [RequireComponent(typeof(Image), typeof(StorageSlotModel))]
    public class StorageSlotView : View, IDragInteractable<IItemStack>
    {
        [SerializeField] protected StorageSlotViewModel storageSlotViewModel = null!;
        [SerializeField] protected Image image = null!;

        public void OnDragged(IItemStack itemStack) => storageSlotViewModel.DragItemStack(itemStack);

        protected override void BindToViewModel() => throw new System.NotImplementedException();
    }
}
