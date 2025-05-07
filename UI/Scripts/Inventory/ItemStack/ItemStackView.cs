using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UTIRLib.Diagnostics;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    [RequireComponent(typeof(Image), typeof(ItemStackModel))]
    public class ItemStackView : View, IDragInteractable<IItemStack>
    {
        [SerializeField] protected ItemStackViewModel itemStackViewModel = null!;
        [SerializeField] protected Transform dragItem = null!;
        [SerializeField] protected Image dragItemImage = null!;
        [SerializeField] protected TextMeshProUGUI quantityTextComponent = null!;
        [SerializeField] protected Image image = null!;

        public void OnDragged(IItemStack itemStack) => itemStackViewModel.PutItemStack(itemStack);

        protected void SetSprite(Sprite? sprite) => image.sprite = sprite;

        protected void SetImageState(bool isEnabled) => image.enabled = isEnabled;

        protected void SetQuantityText(string quantityString) =>
            quantityTextComponent.text = quantityString;

        protected void SetQuantityTextComponentState(bool isEnabled) =>
            quantityTextComponent.enabled = isEnabled;

        protected void SetDragItemStackState(bool isActive) => dragItem.gameObject.SetActive(isActive);

        protected override void BindToViewModel()
        {
            itemStackViewModel.ItemSpriteView.Subscribe(SetSprite).AddTo(this);
            itemStackViewModel.ViewImageEnabled.Subscribe(SetImageState).AddTo(this);
            itemStackViewModel.QuantityTextView.Subscribe(SetQuantityText).AddTo(this);
            itemStackViewModel.QuantityTextComponentState.Subscribe(SetQuantityTextComponentState).AddTo(this);
        }
    }
}
