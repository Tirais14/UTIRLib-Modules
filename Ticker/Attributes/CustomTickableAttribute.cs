using System;
using System.Linq;
using UnityEngine;

namespace UTIRLib.CustomTicker
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = true)]
    public sealed class CustomTickableAttribute : Attribute
    {
        private readonly Type[] tickableInterfaceTypes;
        private readonly Type tickerType;
        public Type[] TickableInterfaceTypes => tickableInterfaceTypes;
        public Type TickerType => tickerType;

        public CustomTickableAttribute(Type tickerType)
        {
            if (!TickerUtility.IsTicker(tickerType)) {
                Debug.LogError($"{tickerType?.Name ?? "null"} not correct ticker type.");
            }
            tickableInterfaceTypes = Array.Empty<Type>();
            this.tickerType = tickerType;
        }
        public CustomTickableAttribute(Type tickableTypeOrInterface, Type tickerType)
        {
            if (!tickableTypeOrInterface.IsInterface && TickableUtility.TryGetTickableInterfaces(tickableTypeOrInterface, out Type[] tickableInterfaceTypes)) {
                this.tickableInterfaceTypes = tickableInterfaceTypes;
            }
            if (!TickableUtility.IsTickable(tickableTypeOrInterface)) {
                Debug.LogError($"{tickableTypeOrInterface?.Name ?? "null"} not correct tickable type.");
            }
            if (!TickerUtility.IsTicker(tickerType)) {
                Debug.LogError($"{tickerType?.Name ?? "null"} not correct ticker type.");
            }

            this.tickableInterfaceTypes = new Type[] { tickableTypeOrInterface };
            this.tickerType = tickerType;
        }
        public CustomTickableAttribute(Type[] tickableInterfaceTypes, Type tickerType)
        {
            if (tickableInterfaceTypes.Any(type => !type.IsInterface)) {
                Debug.LogError($"Tickable interfaces types contains not interface type.");
            }
            if (!tickableInterfaceTypes.All(TickableUtility.IsTickable)) {
                Debug.LogError($"Tickable interfaces types contains not allowed type.");
            }
            if (!TickerUtility.IsTicker(tickerType)) {
                Debug.LogError($"{tickerType.Name} not correct ticker type.");
            }

            this.tickableInterfaceTypes = tickableInterfaceTypes;
            this.tickerType = tickerType;
        }
    }
}