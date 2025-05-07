using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UTIRLib.Attributes;
using UTIRLib.Diagnostics;
using UTIRLib.InputSystem.Enums;

#nullable enable
namespace UTIRLib.InputSystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "InputActionInfo", menuName = "Scriptable Objects/Input Action Info Asset")]
    public class InputActionInfoAsset : ScriptableObject, IInitializable<InputActionAsset, string, InputAction>
    {
        public Type InputActionValueSystemType { get; private set; } = null!;
        [field: SerializeField] public InputActionAsset InputActionsPackageAsset { get; private set; } = null!;
        [field: SerializeField] public string ActionMapName { get; private set; } = null!;
        [field: SerializeField] public string ActionName { get; private set; } = null!;
        [field: SerializeField] public InputActionValueType InputActionValueType { get; private set; }
        [field: SerializeField] public ControlBindingAttributes ControlBindingAttributes { get; private set; }
        public bool IsInitialized { get; private set; }

        public void Initialize(InputActionAsset inputActionPackageAsset, string actionMapName, InputAction inputAction) =>
            InitializeHelper.InitializeObject<Action<InputActionAsset, string, InputAction>>(this, OnInitialize,
                inputActionPackageAsset, actionMapName, inputAction);

        [OnInitialize]
        protected void OnInitialize(InputActionAsset inputActionPackageAsset, string actionMapName, InputAction inputAction)
        {
            InputActionValueSystemType = inputAction.activeValueType;
            InputActionsPackageAsset = inputActionPackageAsset;
            ActionMapName = actionMapName;
            ActionName = inputAction.name;
            SetInputActionValueType(inputAction);
        }

        /// <summary>
        /// <br/>Logs:
        /// <br/><see cref="ArgumentWrongMessage"/>
        /// </summary>
        /// <returns><see langword="null"/> if failured</returns>
        public InputAction? GetInputAction()
        {
            InputAction? foundInputAction = InputActionsPackageAsset.FindActionMap(ActionMapName, true)?.
            FindAction(ActionName);

            if (foundInputAction == null) {
                Debug.LogError(new ArgumentWrongMessage(ActionMapName, nameof(ActionMapName), "Input action doesn't registred."));
            }

            return foundInputAction;
        }

        private void SetInputActionValueType(InputAction inputAction)
        {
            if (inputAction.expectedControlType.Contains(
                nameof(Vector2), StringComparison.InvariantCulture)) {
                InputActionValueType = InputActionValueType.Vector2;
            }
            else if (inputAction.expectedControlType.Contains(
                nameof(Vector3), StringComparison.InvariantCulture)) {
                InputActionValueType = InputActionValueType.Vector3;
            }
            else if (inputAction.expectedControlType.Contains(
                nameof(Quaternion), StringComparison.InvariantCulture)) {
                InputActionValueType = InputActionValueType.Quaternion;
            }
            else if (inputAction.expectedControlType == "Button" || inputAction.activeValueType == typeof(bool) ||
                inputAction.type == InputActionType.Button) {
                InputActionValueType = InputActionValueType.Button;
            }
            else {
                InputActionValueType = InputActionValueType.None;
            }
        }
    }
}