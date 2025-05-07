using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using UnityEditor.AddressableAssets.Settings;
using UTIRLib.Database;
using UTIRLib.Enums;

#nullable enable
namespace UTIRLib
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class AddressableAssetInfo
    {
        [JsonRequired] private readonly string name;
        [JsonRequired] private readonly string guid;
        [JsonRequired] private readonly string address;
        private readonly string labels;
        [JsonRequired] private readonly string assetTypeName;
        [JsonIgnore] private readonly AssetTypeName assetType;
        [JsonIgnore] public string Name => name;
        [JsonIgnore] public string AssetGUID => guid;
        [JsonIgnore] public string Address => address;
        [JsonIgnore] public string[] Labels => GetLabels();
        [JsonIgnore] public AssetTypeName AssetType => assetType;

        /// <exception cref="ArgumentNullException"></exception>
        public AddressableAssetInfo(AddressableAssetEntry addressableAssetEntry)
        {
            UnityEngine.Object tagetAsset = addressableAssetEntry.TargetAsset;

            name = tagetAsset.name;
            guid = addressableAssetEntry.guid;
            address = addressableAssetEntry.address;
            labels = LabelsToString(addressableAssetEntry.labels);
            assetType = TirLibConvert.SystemTypeToAssetType(tagetAsset.GetType());

            assetTypeName = assetType.ToString();
        }
        [JsonConstructor]
        public AddressableAssetInfo(string name, string guid, string address,
            string labels, string assetTypeName)
        {
            this.name = name;
            this.guid = guid;
            this.address = address;
            this.labels = labels;
            this.assetTypeName = assetTypeName;
            assetType = Enum.Parse<AssetTypeName>(assetTypeName);
        }

        public string[] GetLabels()
        {
            if (string.IsNullOrEmpty(labels)) {
                return Array.Empty<string>();
            }

            return labels.Split(", ");
        }

        public override string ToString() => $"{name} ({assetType}).\n(labels: {labels})";

        private IAssetDatabaseItem CreateAssetDatabaseItem(AssetTypeName assetType) =>
            assetType switch {
                AssetTypeName.GameObject => ConstructAssetDatabaseItem(typeof(AssetDatabaseItemGameObject)),
                AssetTypeName.ScriptableObject => ConstructAssetDatabaseItem(typeof(AssetDatabaseItemScriptableObject)),
                AssetTypeName.Scene => ConstructAssetDatabaseItem(typeof(AssetDatabaseItemScene)),
                _ => ConstructAssetDatabaseItem(typeof(AssetDatabaseItemGeneric)),
            };

        private IAssetDatabaseItem ConstructAssetDatabaseItem(Type type)
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            Type[] constructorArgTypes = new Type[] { typeof(AddressableAssetInfo) };
            var constructorArgs = new object[] { this };

            return type.GetConstructor(bindingFlags, binder: null, constructorArgTypes, Array.Empty<ParameterModifier>()).
                Invoke(constructorArgs) as IAssetDatabaseItem ?? throw new NullReferenceException("Error while castong.");
        }

        private static string LabelsToString(HashSet<string> labels)
        {
            if (labels == null || labels.Count == 0) return string.Empty;

            string[] labelsArray = labels.ToArray();
            StringBuilder labelsStringBuilder = new();
            int labelsCount = labelsArray.Length;
            for (int i = 0; i < labelsCount; i++) {
                if ((i + 1) < labelsCount) { labelsStringBuilder.Append($"{labelsArray[i]}, "); }
                else { labelsStringBuilder.Append($"{labelsArray[i]}"); }
            }

            return labelsStringBuilder.ToString();
        }

        public static explicit operator KeyObjectPair(AddressableAssetInfo addressableAssetInfo) =>
            new(addressableAssetInfo.Name, addressableAssetInfo.CreateAssetDatabaseItem(addressableAssetInfo.assetType));
    }
}