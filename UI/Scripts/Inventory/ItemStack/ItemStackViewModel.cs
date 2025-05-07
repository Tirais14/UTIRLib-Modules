using UniRx;
using UnityEngine;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class ItemStackViewModel : ViewModel
    {
        protected readonly ReactiveProperty<Sprite?> itemSpriteView = new();
        protected readonly ReactiveProperty<bool> viewImageEnabled = new();
        protected readonly ReactiveProperty<string> quantityTextView = new();
        protected readonly ReactiveProperty<bool> quantityTextComponentState = new();
        [SerializeField] protected ItemStackModel itemStack = null!;

        public IReadOnlyReactiveProperty<Sprite?> ItemSpriteView => itemSpriteView;
        public IReadOnlyReactiveProperty<bool> ViewImageEnabled => viewImageEnabled;
        public IReadOnlyReactiveProperty<string> QuantityTextView => quantityTextView;
        public IReadOnlyReactiveProperty<bool> QuantityTextComponentState => quantityTextComponentState;


        public void PutItemStack(IItemStack itemStack)
        {
            if (itemStack == null) {
                return;
            }

            this.itemStack.PutItemStack(itemStack);
        }

        /// <returns>Remaining items or <see langword="null"/></returns>
        public IItemStack TakeItemStack(int quantity)
        {
            if (itemStack.IsEmpty) return ItemStack.Empty;

            return itemStack.TakeItem(quantity);
        }

        public void SetItemView(IItem? item)
        {
            itemSpriteView.Value = item?.Sprite;
            viewImageEnabled.Value = item != null;
        }

        public void SetQuantityView(int count)
        {
            quantityTextView.Value = count.ToString();
            quantityTextComponentState.Value = count > 0;
        }

        protected override void BindToModel()
        {
            itemStack.ItemProp.Subscribe(SetItemView).AddTo(this);
            itemStack.QuantityProp.Subscribe(SetQuantityView).AddTo(this);
        }
    }
}
