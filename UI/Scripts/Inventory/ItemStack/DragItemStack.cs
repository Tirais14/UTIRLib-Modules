using UnityEngine;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class DragItemStack : MonoBehaviourExtended
    {
        [SerializeField] protected ItemStackModel sourceItemStack = null!;
        [SerializeField] protected ItemStackView sourceItemStackView= null!;

        public IItemStack SourceItemStack => sourceItemStack;
        public IDragInteractable<IItemStack> SourceItemStackView => sourceItemStackView;
    }
}
