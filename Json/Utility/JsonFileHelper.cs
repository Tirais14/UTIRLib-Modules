using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UTIRLib.Diagnostics;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
#pragma warning disable S2955 // Generic parameters not constrained to reference types should not be compared to "null"
namespace UTIRLib.Json
{
	public static class JsonFileHelper
	{
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static string Serialize(object obj, bool formatText, bool checkAttributes, IEnumerable<Type> attributesForCheck,
            bool mustMatchAllAttributes = false, JsonSerializerSettings? settings = null)
        {
            if (obj == null) {
                throw new ArgumentNullException(nameof(obj));
            }
            if (obj is string str && string.IsNullOrEmpty(str)) {
                Debug.LogError(new NullOrEmptyStringMessage(str, nameof(str)));
            }

            if (checkAttributes && !DoCheckAttributes(obj.GetType(), attributesForCheck, mustMatchAllAttributes)) {
                return string.Empty;
            }

            string serialized;
            if (settings != null) {
                serialized = formatText ?
                JsonConvert.SerializeObject(obj, Formatting.Indented, settings) :
                JsonConvert.SerializeObject(obj, Formatting.None, settings);
            }
            else {
                serialized = formatText ?
                JsonConvert.SerializeObject(obj, Formatting.Indented) :
                JsonConvert.SerializeObject(obj, Formatting.None);
            }

            if (string.IsNullOrEmpty(serialized)) {
                throw new NullOrEmptyStringException(serialized, nameof(serialized));
            }

            return serialized;
        }
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EmptyCollectionException"></exception>
        /// <exception cref="NullOrEmptyStringException"></exception>
        public static string Serialize<T>(T obj, bool formatText, bool checkAttributes, IEnumerable<Type> attributesForCheck,
            bool mustMatchAllAttributes = false, JsonSerializerSettings? settings = null)
        {
            if (obj == null || obj is IEnumerable<T> typedEnumerable && typedEnumerable.Count() == 0) {
                throw new NullOrEmptyCollectionException(obj, nameof(obj));
            }
            if (obj is string str && string.IsNullOrEmpty(str)) {
                Debug.LogError(new NullOrEmptyStringMessage(str, nameof(str)));
            }

            if (checkAttributes && !DoCheckAttributes(typeof(T), attributesForCheck, mustMatchAllAttributes)) {
                return string.Empty;
            }

            string serialized;
            if (settings != null) {
                serialized = formatText ?
                JsonConvert.SerializeObject(obj, Formatting.Indented, settings) :
                JsonConvert.SerializeObject(obj, Formatting.None, settings);
            }
            else {
                serialized = formatText ?
                JsonConvert.SerializeObject(obj, Formatting.Indented) :
                JsonConvert.SerializeObject(obj, Formatting.None);
            }

            if (string.IsNullOrEmpty(serialized)) {
                throw new NullOrEmptyStringException(serialized, nameof(serialized));
            }

            return serialized;
        }

        /// <exception cref="NullOrEmptyStringException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static object? Deserialize(string content, Type objType, IEnumerable<Type>? attributesForCheck = null,
            bool mathcAllAttributes = false)
        {
            if (string.IsNullOrEmpty(content)) {
                throw new NullOrEmptyStringException(nameof(content));
            }
            if (objType == null) {
                throw new ArgumentNullException(nameof(objType));
            }
            if (attributesForCheck != null) {
                DoCheckAttributes(objType, attributesForCheck, mathcAllAttributes);
                return null;
            }

            object? deserialized = JsonConvert.DeserializeObject(content, objType);
            if (deserialized == null) {
                throw new NullReferenceException(nameof(deserialized));
            }

            return deserialized;
        }

        /// <exception cref="ArgumentNullException"></exception>
        public static bool DoCheckAttributes(Type objType, IEnumerable<Type> attributesForCheck, bool mustMatchAllAttributes)
        {
            if (attributesForCheck == null) {
                throw new ArgumentNullException(nameof(attributesForCheck));
            }

            bool result = true;
            foreach (var attributeType in attributesForCheck) {
                if (!objType.IsDefined(attributeType, true)) {
                    Debug.LogError($"{objType.Name} doesn't contain attribute {attributeType.Name}");
                    if (mustMatchAllAttributes) {
                        break;
                    }
                    result = false;
                }
            }

            return result;
        }
    }
}
