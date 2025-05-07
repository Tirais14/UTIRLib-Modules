using System;
using UnityEngine;
using UTIRLib.InputSystem.Enums;

#nullable enable
namespace UTIRLib
{
    public static partial class TirLibConvert
    {
        public static InputActionValueType SystemTypeToInputActionValueType(Type? type)
        {
            if (type == null) {
                return InputActionValueType.None;
            }
            else if (type == typeof(bool)) {
                return InputActionValueType.Button;
            }
            else if (type == typeof(Vector2)) {
                return InputActionValueType.Vector2;
            }
            else if (type == typeof(Vector3)) {
                return InputActionValueType.Vector3;
            }
            else if (type == typeof(Quaternion)) {
                return InputActionValueType.Quaternion;
            }
            else {
                return InputActionValueType.None;
            }
        }
    }
}
