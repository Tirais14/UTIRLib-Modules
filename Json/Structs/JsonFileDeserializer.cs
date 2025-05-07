using System;
using System.Collections.Generic;
using UnityEngine;

namespace UTIRLib.Json.Structs
{
    public struct JsonFileDeserializer
    {
        private object deserialized;
        private HashSet<Type> attributesForCheck;
        private bool isSerialized;
        public string toDeserialize;
        public Type toDeserializeType;
        public bool checkAttributes;
        public bool mustMatchAllAttributes;

        public readonly object Deserialized => deserialized;
        public readonly HashSet<Type> AttributesForCheck => attributesForCheck;
        public readonly bool IsSerialized => isSerialized;

        public JsonFileDeserializer(string toDeserialize, Type toDeserializeType, bool checkAttributes = false, bool mustMatchAllAttributes = false, params Type[] attributesForCheck)
        {
            deserialized = null;
            this.toDeserializeType = toDeserializeType;
            isSerialized = false;
            this.toDeserialize = toDeserialize;
            this.attributesForCheck = new HashSet<Type>(attributesForCheck ?? Array.Empty<Type>());
            this.checkAttributes = checkAttributes;
            this.mustMatchAllAttributes = mustMatchAllAttributes;
        }

        public void Deserialize()
        {
            try {
                deserialized = JsonFileHelper.Deserialize(toDeserialize, toDeserializeType, attributesForCheck,
                    mustMatchAllAttributes);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
            }

            isSerialized = deserialized != null;
        }
    }
    public struct JsonFileDeserializer<T>
    {
        private T deserialized;
        private HashSet<Type> attributesForCheck;
        private bool isSerialized;
        public string toDeserialize;
        public bool checkAttributes;
        public bool mustMatchAllAttributes;

        public readonly T Deserialized => deserialized;
        public readonly HashSet<Type> AttributesForCheck => attributesForCheck;
        public readonly bool IsSerialized => isSerialized;   

        public JsonFileDeserializer(string toDeserialize, bool checkAttributes = false, bool mustMatchAllAttributes = false, params Type[] attributesForCheck)
        {
            deserialized = default;
            isSerialized = false;
            this.toDeserialize = toDeserialize;
            this.attributesForCheck = new HashSet<Type>(attributesForCheck ?? Array.Empty<Type>());
            this.checkAttributes = checkAttributes;
            this.mustMatchAllAttributes = mustMatchAllAttributes;
        }

        public void Deserialize()
        {
            try {
                deserialized = (T)JsonFileHelper.Deserialize(toDeserialize, typeof(T), attributesForCheck,
                    mustMatchAllAttributes);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
            }

            isSerialized = deserialized != null;
        }
    }
}
