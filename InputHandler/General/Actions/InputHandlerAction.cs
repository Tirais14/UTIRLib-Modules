using System;
using System.Collections;
using ModestTree.Util;
using UnityEngine.InputSystem;

#nullable enable
namespace UTIRLib.InputSystem
{
    public class InputHandlerAction<TInputValue> : IInputHandlerAction<TInputValue>
        where TInputValue : struct
    {
        protected const int ACTION_COUNT = 9;
        protected const int ON_PERFORMED_ACTION_ID = 0;
        protected const int ON_STARTED_ACTION_ID = 0;
        protected const int ON_CANCELLED_ACTION_ID = 0;
        protected const int RAW_ACTIONS_ID_OFFSET = 1;
        protected const int ACTIONS_WTIH_VALUE_ID_OFFSET = 2;
        protected const int ACTIONS_ID_OFFSET = 3;

        protected readonly InputAction inputAction;
        protected readonly string inputActionName;
        protected BitArray enabledActionsMask = new(ACTION_COUNT);
        protected Action<InputAction.CallbackContext>? rawOnPerformed;
        protected Action<InputAction.CallbackContext>? rawOnStarted;
        protected Action<InputAction.CallbackContext>? rawOnCancelled;
        protected Action<TInputValue>? onPerformedWithValue;
        protected Action<TInputValue>? onStartedWithValue;
        protected Action<TInputValue>? onCancelledWithValue;
        protected Action? onPerformed;
        protected Action? onStarted;
        protected Action? onCancelled;

        public event Action<InputAction.CallbackContext> RawOnPerformed{
            add {
                rawOnPerformed += value;
                SetMaskValue(rawOnPerformed, ON_PERFORMED_ACTION_ID);
            }
            remove {
                rawOnPerformed -= value;
                SetMaskValue(rawOnPerformed, ON_PERFORMED_ACTION_ID);
            }
        }
        public event Action<InputAction.CallbackContext> RawOnStarted{
            add {
                rawOnStarted += value;
                SetMaskValue(rawOnStarted, ON_STARTED_ACTION_ID);
            }
            remove {
                rawOnStarted -= value;
                SetMaskValue(rawOnStarted, ON_STARTED_ACTION_ID);
            }
        }
        public event Action<InputAction.CallbackContext> RawOnCancelled{
            add {
                rawOnCancelled += value;
                SetMaskValue(rawOnCancelled, ON_CANCELLED_ACTION_ID);
            }
            remove {
                rawOnCancelled -= value;
                SetMaskValue(rawOnCancelled, ON_CANCELLED_ACTION_ID);
            }
        }
        public event Action<TInputValue> OnPerformedWithValue{
            add {
                onPerformedWithValue += value;
                SetMaskValue(onPerformedWithValue, ON_PERFORMED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET);
            }
            remove {
                onPerformedWithValue -= value;
                SetMaskValue(onPerformedWithValue, ON_PERFORMED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET);
            }
        }
        public event Action<TInputValue> OnStartedWithValue{
            add {
                onStartedWithValue += value;
                SetMaskValue(onStartedWithValue, ON_STARTED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET);
            }
            remove {
                onStartedWithValue -= value;
                SetMaskValue(onStartedWithValue, ON_STARTED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET);
            }
        }
        public event Action<TInputValue> OnCancelledWithValue {
            add {
                onCancelledWithValue += value;
                SetMaskValue(onCancelledWithValue, ON_CANCELLED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET);
            }
            remove {
                onCancelledWithValue -= value;
                SetMaskValue(onCancelledWithValue, ON_CANCELLED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET);
            }
        }
        public event Action OnPerformed{
            add {
                onPerformed += value;
                SetMaskValue(onPerformed, ON_PERFORMED_ACTION_ID, ACTIONS_ID_OFFSET);
            }
            remove {
                onPerformed -= value;
                SetMaskValue(onPerformed, ON_PERFORMED_ACTION_ID, ACTIONS_ID_OFFSET);
            }
        }
        public event Action OnStarted{
            add {
                onStarted += value;
                SetMaskValue(onStarted, ON_STARTED_ACTION_ID, ACTIONS_ID_OFFSET);
            }
            remove {
                onStarted -= value;
                SetMaskValue(onStarted, ON_STARTED_ACTION_ID, ACTIONS_ID_OFFSET);
            }
        }
        public event Action OnCancelled {
            add {
                onCancelled += value;
                SetMaskValue(onCancelled, ON_CANCELLED_ACTION_ID, ACTIONS_ID_OFFSET);
            }
            remove {
                onCancelled -= value;
                SetMaskValue(onCancelled, ON_CANCELLED_ACTION_ID, ACTIONS_ID_OFFSET);
            }
        }
        public InputAction InputAction => inputAction;
        public string InputActionName => inputActionName;

        public InputHandlerAction(InputAction inputAction)
        {
            this.inputAction = inputAction ?? throw new ArgumentNullException(nameof(inputAction));
            inputActionName = inputAction.name;
        }

        public virtual void OnPerformedAction(InputAction.CallbackContext context)
        {
            if (GetMaskValue(ON_PERFORMED_ACTION_ID)) {
                rawOnPerformed!(context);
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET)) {
                onPerformedWithValue!(context.ReadValue<TInputValue>());
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_ID_OFFSET)) {
                onPerformed!();
            }
        }

        public virtual void OnStartedAction(InputAction.CallbackContext context)
        {
            if (GetMaskValue(ON_PERFORMED_ACTION_ID)) {
                rawOnStarted!(context);
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET)) {
                onStartedWithValue!(context.ReadValue<TInputValue>());
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_ID_OFFSET)) {
                onStarted!();
            }
        }

        public virtual void OnCancelledAction(InputAction.CallbackContext context)
        {
            if (GetMaskValue(ON_PERFORMED_ACTION_ID)) {
                rawOnCancelled!(context);
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET)) {
                onCancelledWithValue!(context.ReadValue<TInputValue>());
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_ID_OFFSET)) {
                onCancelled!();
            }
        }

        public void Subscribe(Delegate action, InputActionEventType eventType)
        {
            switch (action) {
                case Action<InputAction.CallbackContext> rawAction:
                    switch (eventType) {
                        case InputActionEventType.OnPerformed:
                            rawOnPerformed += rawAction;
                            break;
                        case InputActionEventType.OnStarted:
                            rawOnStarted += rawAction;
                            break;
                        case InputActionEventType.OnCancelled:
                            rawOnCancelled += rawAction;
                            break;
                        default:
                            break;
                    }
                    break;
                case Action<TInputValue> actionWithValue:
                    switch (eventType) {
                        case InputActionEventType.OnPerformed:
                            onPerformedWithValue += actionWithValue;
                            break;
                        case InputActionEventType.OnStarted:
                            onStartedWithValue += actionWithValue;
                            break;
                        case InputActionEventType.OnCancelled:
                            onCancelledWithValue += actionWithValue;
                            break;
                        default:
                            break;
                    }
                    break;
                case Action basicAction:
                    switch (eventType) {
                        case InputActionEventType.OnPerformed:
                            onPerformed += basicAction;
                            break;
                        case InputActionEventType.OnStarted:
                            onStarted += basicAction;
                            break;
                        case InputActionEventType.OnCancelled:
                            onCancelled += basicAction;
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        public void Unsubscribe(Delegate action, InputActionEventType eventType)
        {
            switch (action) {
                case Action<InputAction.CallbackContext> rawAction:
                    switch (eventType) {
                        case InputActionEventType.OnPerformed:
                            rawOnPerformed -= rawAction;
                            break;
                        case InputActionEventType.OnStarted:
                            rawOnStarted -= rawAction;
                            break;
                        case InputActionEventType.OnCancelled:
                            rawOnCancelled -= rawAction;
                            break;
                        default:
                            break;
                    }
                    break;
                case Action<TInputValue> actionWithValue:
                    switch (eventType) {
                        case InputActionEventType.OnPerformed:
                            onPerformedWithValue -= actionWithValue;
                            break;
                        case InputActionEventType.OnStarted:
                            onStartedWithValue -= actionWithValue;
                            break;
                        case InputActionEventType.OnCancelled:
                            onCancelledWithValue -= actionWithValue;
                            break;
                        default:
                            break;
                    }
                    break;
                case Action basicAction:
                    switch (eventType) {
                        case InputActionEventType.OnPerformed:
                            onPerformed -= basicAction;
                            break;
                        case InputActionEventType.OnStarted:
                            onStarted -= basicAction;
                            break;
                        case InputActionEventType.OnCancelled:
                            onCancelled -= basicAction;
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        protected bool GetMaskValue(int actionId, int actionIdOffset = ON_PERFORMED_ACTION_ID) => enabledActionsMask[actionId * actionIdOffset];

        protected void SetMaskValue(Delegate? action, int actionId, int actionIdOffset = ON_PERFORMED_ACTION_ID) =>
            enabledActionsMask[actionId * actionIdOffset] = action != null;
    }
}
