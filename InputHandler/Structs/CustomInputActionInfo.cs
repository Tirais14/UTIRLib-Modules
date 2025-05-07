using System;
using Newtonsoft.Json;
using UTIRLib.InputSystem.Enums;

#nullable enable
namespace UTIRLib.InputSystem.Structs
{

    [Serializable]
    [JsonObject]
    public struct CustomInputActionInfo
    {
        [JsonRequired] public Type InputActionValueSystemType { readonly get; private set; }
        [JsonRequired] public string Name { readonly get; set; }
        [JsonRequired] public InputActionValueType InputActionValueType { readonly get; set; }
        [JsonRequired] public ControlBindingAttributes ControlBindingAttributes { readonly get; set; }

        [JsonConstructor]
        public CustomInputActionInfo(string name, InputActionValueType inputActionValueType,
            ControlBindingAttributes attributes)
        {
            InputActionValueSystemType = inputActionValueType.ToSystemType() ?? throw new NullReferenceException();
            Name = name;
            InputActionValueType = inputActionValueType;
            ControlBindingAttributes = attributes;
        }
    }
}
