using System;
using UnityEngine.InputSystem;

#nullable enable
namespace UTIRLib.InputSystem
{
    public interface IInputHandlerAction
    {
        public event Action<InputAction.CallbackContext> RawOnPerformed;
        public event Action<InputAction.CallbackContext> RawOnStarted;
        public event Action<InputAction.CallbackContext> RawOnCancelled;
        public event Action OnPerformed;
        public event Action OnStarted;
        public event Action OnCancelled;

        public void Subscribe(Delegate action, InputActionEventType eventType);

        public void Unsubscribe(Delegate action, InputActionEventType eventType);

        public void OnPerformedAction(InputAction.CallbackContext context);

        public void OnStartedAction(InputAction.CallbackContext context);

        public void OnCancelledAction(InputAction.CallbackContext context);
    }
    public interface IInputHandlerAction<TInputValue> : IInputHandlerAction
        where TInputValue : struct
    {
        public event Action<TInputValue> OnPerformedWithValue;
        public event Action<TInputValue> OnStartedWithValue;
        public event Action<TInputValue> OnCancelledWithValue;
    }
}
