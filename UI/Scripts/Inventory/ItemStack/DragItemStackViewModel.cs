using UniRx;
using UnityEngine;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    public class DragItemStackViewModel : ViewModel
    {
        protected readonly ReactiveProperty<Color> imageColorView = new();
        protected readonly ReactiveProperty<Sprite?> imageSpriteView = new();
        [SerializeField] protected DragItemStack dragItemStack = null!;

        public bool IsActive => dragItemStack.SourceItemStack.IsNotEmpty;
        public bool IsNotActive => dragItemStack.SourceItemStack.IsEmpty;
        public IReadOnlyReactiveProperty<Color> ImageColorView => imageColorView;
        public IReadOnlyReactiveProperty<Sprite?> ImageSpriteView => imageSpriteView;
        public IItemStack SourceItemStack => dragItemStack.SourceItemStack;
        public IDragInteractable<IItemStack> SourceItemStackView => dragItemStack.SourceItemStackView;

        public void OnBeginDrag()
        {
            imageColorView.Value = Color.white;
            imageSpriteView.Value = dragItemStack.SourceItemStack.Item?.Sprite;
        }

        public void OnEndDrag()
        {
            imageColorView.Value = Color.clear;
            imageSpriteView.Value = null;
        }

        protected override void BindToModel() { }
    }
}
