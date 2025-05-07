using UnityEngine;
using UTIRLib.InputSystem;

#nullable enable
namespace UTIRLib.UI
{
    public abstract class DraggableViewBase : View, IDraggable
    {
        protected readonly Vector2 defaultPosition = new();
        protected IPointerHandler pointerHandler = null!;
        protected IRaycaster raycaster = null!;

        [SerializeField] protected UserInterface userInterface = null!;

        protected override void OnAwake()
        {
            base.OnAwake();
            pointerHandler = userInterface.PointerHandler;
            raycaster = userInterface.Raycaster;
        }

        public virtual void OnDrag() => FollowPointer();

        protected void FollowPointer() => transform.position = pointerHandler.PointerPosition;

        protected void ResetPosition() => transform.localPosition = defaultPosition;
    }
}
