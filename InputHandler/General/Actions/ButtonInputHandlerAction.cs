using UnityEngine.InputSystem;

#nullable enable
namespace UTIRLib.InputSystem
{
    public class ButtonInputHandlerAction : InputHandlerAction<bool>
    {
        public ButtonInputHandlerAction(InputAction inputAction) : base(inputAction)
        { }

        public override void OnPerformedAction(InputAction.CallbackContext context)
        {
            if (GetMaskValue(ON_PERFORMED_ACTION_ID)) {
                rawOnPerformed!(context);
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET)) {
                onPerformedWithValue!(context.ReadValueAsButton());
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_ID_OFFSET)) {
                onPerformed!();
            }
        }

        public override void OnStartedAction(InputAction.CallbackContext context)
        {
            if (GetMaskValue(ON_PERFORMED_ACTION_ID)) {
                rawOnStarted!(context);
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET)) {
                onStartedWithValue!(context.ReadValueAsButton());
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_ID_OFFSET)) {
                onStarted!();
            }
        }

        public override void OnCancelledAction(InputAction.CallbackContext context)
        {
            if (GetMaskValue(ON_PERFORMED_ACTION_ID)) {
                rawOnCancelled!(context);
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_WTIH_VALUE_ID_OFFSET)) {
                onCancelledWithValue!(context.ReadValueAsButton());
            }
            if (GetMaskValue(ON_PERFORMED_ACTION_ID, ACTIONS_ID_OFFSET)) {
                onCancelled!();
            }
        }
    }
}
