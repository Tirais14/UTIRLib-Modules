using UniRx;
using UnityEngine;
using UnityEngine.UI;

#nullable enable
namespace UTIRLib.UI.InventorySystem
{
    [RequireComponent(typeof(DragItemStack), typeof(Image))]
    public class DragItemStackView : DraggableViewBase, IDraggableBegin, IDraggable, IDraggableEnd
    {
        [SerializeField] protected DragItemStackViewModel dragItemStackViewModel = null!;
        [SerializeField] protected Image image = null!;

        public void OnBeginDrag()
        {
            if (dragItemStackViewModel.IsNotActive) return;

            dragItemStackViewModel.OnBeginDrag();
        }

        public override void OnDrag()
        {
            if (dragItemStackViewModel.IsNotActive) return;

            FollowPointer();
        }

        public void OnEndDrag()
        {
            if (dragItemStackViewModel.IsNotActive) return;

            TryPutItemStack();
            dragItemStackViewModel.OnEndDrag();
            ResetPosition();
        }

        protected void TryPutItemStack()
        {
            if (raycaster.TryRaycastFirst(out IDragInteractable<IItemStack>? interactable,
                dragItemStackViewModel.SourceItemStackView)) {
                interactable.OnDragged(dragItemStackViewModel.SourceItemStack);
            }
        }

        protected void SetImageColor(Color color) => image.color = color;

        protected void SetImageSprite(Sprite? sprite) => image.sprite = sprite;

        protected override void BindToViewModel()
        {
            dragItemStackViewModel.ImageColorView.Subscribe(SetImageColor).AddTo(this);
            dragItemStackViewModel.ImageSpriteView.Subscribe(SetImageSprite).AddTo(this);
        }
    }
}
