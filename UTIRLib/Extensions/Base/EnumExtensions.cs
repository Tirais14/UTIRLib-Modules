using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UTIRLib.Attributes;

#nullable enable
namespace UTIRLib.Enums
{
    public static class EnumExtensions
    {
        private readonly static Dictionary<Enum, string> cachedAttributeCustomStrings = new();

        public static string GetName<T>(this T enumValue) where T : Enum => Enum.GetName(enumValue.GetType(), enumValue);
        /// <summary>
        /// Gets custom string from <see cref="CustomStringAttribute"/> 
        /// by reflection and caches them for next time
        /// </summary>
        /// <returns>default or custom <see cref="string"/></returns>
        public static string ToAttributeCustomString<T>(this T enumValue)
            where T : Enum
        {
            if (cachedAttributeCustomStrings.TryGetValue(enumValue, out string enumName)) {
                return enumName;
            }

            enumName = enumValue.ToString();
            CustomStringAttribute? customStringAttribute = enumValue.GetType().GetField(enumName)?
                .GetCustomAttribute<CustomStringAttribute>();

            if (customStringAttribute != null) {
                cachedAttributeCustomStrings.Add(enumValue, customStringAttribute.Value);
                return customStringAttribute.Value;
            }
            else return enumName;
        }

        public static T GetUndelyingValue<T>(this Enum value)
            where T : struct
        {
            try {
                if (Convert.ChangeType(value, typeof(T)) is T convertedValue) {
                    return convertedValue;
                }
            }
            catch (Exception ex) {
                Debug.LogException(ex);
            }

            return default;
        }
    }
}
