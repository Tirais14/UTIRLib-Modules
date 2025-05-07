using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace UTIRLib.Json.Structs
{
    public struct JsonFileSerializer
    {
        private string serialized;
        public object obj;
        public bool formatText;
        public bool checkAttributes;
        public bool mustMatchAllAttributes;
        public JsonSerializerSettings settings;
        public HashSet<Type> attributesForCheck;

        public readonly string Serialized => serialized;

        public JsonFileSerializer(object obj, bool formatText = true, bool checkAttributes = true,
            bool mustMatchAllAttributes = false, JsonConverter[] converters = null)
        {
            serialized = string.Empty;
            this.obj = obj;
            this.formatText = formatText;
            this.checkAttributes = checkAttributes;
            this.mustMatchAllAttributes = mustMatchAllAttributes;
            settings = new JsonSerializerSettings() {
                Converters = converters ?? Array.Empty<JsonConverter>()
            };
            attributesForCheck = new HashSet<Type>() {
                typeof(SerializableAttribute),
                typeof(JsonObjectAttribute)
            };
        }
        public JsonFileSerializer(object obj, bool formatText = true, bool checkAttributes = true,
            bool mustMatchAllAttributes = false, JsonSerializerSettings settings = null)
        {
            serialized = string.Empty;
            this.obj = obj;
            this.formatText = formatText;
            this.checkAttributes = checkAttributes;
            this.mustMatchAllAttributes = mustMatchAllAttributes;
            this.settings = settings;
            attributesForCheck = new HashSet<Type>() {
                typeof(SerializableAttribute),
                typeof(JsonObjectAttribute)
            };
        }

        public void Serialize()
        {
            try {
                serialized = JsonFileHelper.Serialize(obj, formatText, checkAttributes,
                                attributesForCheck, mustMatchAllAttributes, settings);
            }
            catch (System.Exception ex) {
                Debug.LogException(ex);
            }
        }
    }
    public struct JsonFileSerializer<T>
    {
        private string serialized;
        public T obj;
        public bool formatText;
        public bool checkAttributes;
        public bool mustMatchAllAttributes;
        public JsonSerializerSettings settings;
        public HashSet<Type> attributesForCheck;

        public readonly string Serialized => serialized;

        public JsonFileSerializer(T obj, bool formatText = true, bool checkAttributes = true,
            bool mustMatchAllAttributes = false, JsonConverter[] converters = null)
        {
            serialized = string.Empty;
            this.obj = obj;
            this.formatText = formatText;
            this.checkAttributes = checkAttributes;
            this.mustMatchAllAttributes = mustMatchAllAttributes;
            settings = new JsonSerializerSettings() {
                Converters = converters ?? Array.Empty<JsonConverter>()
            };
            attributesForCheck = new HashSet<Type>() {
                typeof(SerializableAttribute),
                typeof(JsonObjectAttribute)
            };
        }
        public JsonFileSerializer(T obj, bool formatText = true, bool checkAttributes = true,
            bool mustMatchAllAttributes = false, JsonSerializerSettings settings = null)
        {
            serialized = string.Empty;
            this.obj = obj;
            this.formatText = formatText;
            this.checkAttributes = checkAttributes;
            this.mustMatchAllAttributes = mustMatchAllAttributes;
            this.settings = settings;
            attributesForCheck = new HashSet<Type>() {
                typeof(SerializableAttribute),
                typeof(JsonObjectAttribute)
            };
        }

        public void Serialize()
        {
            try {
                serialized = JsonFileHelper.Serialize(obj, formatText, checkAttributes,
                                attributesForCheck, mustMatchAllAttributes, settings);
            }
            catch (Exception ex) {
                Debug.LogException(ex);
            }
        }
    }
}