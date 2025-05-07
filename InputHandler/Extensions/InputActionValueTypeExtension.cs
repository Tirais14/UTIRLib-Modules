using System;
using UnityEngine;

#nullable enable
namespace UTIRLib.InputSystem
{
    using Enums;

    public static class InputActionValueTypeExtension
    {
        public static Type? ToSystemType(this InputActionValueType inputActionValueType) =>
            inputActionValueType switch {
                InputActionValueType.None => null,
                InputActionValueType.Button => typeof(bool),
                InputActionValueType.Vector2 => typeof(Vector2),
                InputActionValueType.Vector3 => typeof(Vector3),
                InputActionValueType.Quaternion => typeof(Quaternion),
                _ => null,
            };
    }
}