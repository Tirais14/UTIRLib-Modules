using System;
using UTIRLib.Enums;
using UTIRLib.Diagnostics.Exceptions;

#nullable enable
namespace UTIRLib.Database
{
    public record DatabaseFileInfo
    {
        private readonly string databaseFilePath;
        private readonly string databaseName;
        private readonly bool loadAssetsImmediately;
        private readonly AssetTypeName databaseAssetType;
        public string DatabaseFilePath => databaseFilePath;
        public string DatabaseName => databaseName;
        public bool LoadAssetsImmediately => loadAssetsImmediately;
        public AssetTypeName DatabaseAssetType => databaseAssetType;

        /// <exception cref="NullOrEmptyStringException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public DatabaseFileInfo(string databaseFilePath, string databaseName, AssetTypeName databaseAssetType,
            bool loadAssetsImmediately = false)
        {
            if (string.IsNullOrEmpty(databaseFilePath)) {
                throw new NullOrEmptyStringException(databaseFilePath, nameof(databaseFilePath));
            }
            if (string.IsNullOrEmpty(databaseName)) {
                throw new NullOrEmptyStringException(databaseName, nameof(databaseName));
            }
            if (databaseAssetType == AssetTypeName.None) {
                throw new ArgumentException($"Value {nameof(databaseFilePath)} cannot contain {AssetTypeName.None}");
            }

            this.databaseFilePath = databaseFilePath;
            this.databaseName = databaseName;
            this.loadAssetsImmediately = loadAssetsImmediately;
            this.databaseAssetType = databaseAssetType;
        }
    }
}