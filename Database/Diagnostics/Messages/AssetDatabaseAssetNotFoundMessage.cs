using System;
using UTIRLib.Diagnostics;

namespace UTIRLib.Database.Diagnostics
{
    public class AssetDatabaseAssetNotFoundMessage : ConsoleMessage
    {
        public AssetDatabaseAssetNotFoundMessage() :
            base($"Not found asset with name.", CallStackFramesOffsetConstructor)
        { }
        public AssetDatabaseAssetNotFoundMessage(string assetName) :
            base($"Not found asset with name {assetName}.", CallStackFramesOffsetConstructor)
        { }
        public AssetDatabaseAssetNotFoundMessage(string assetName, Type assetType) :
            base($"Not found asset {assetName} {assetType.Name}.", CallStackFramesOffsetConstructor)
        { }
        public AssetDatabaseAssetNotFoundMessage(object databaseGroup, string assetName) :
            base($"Not found asset {assetName} in group {databaseGroup.GetType().Name}.",
                CallStackFramesOffsetConstructor)
        { }
        public AssetDatabaseAssetNotFoundMessage(object databaseGroup, string assetName, Type assetType) :
            base($"Not found asset {assetName} {assetType.Name} in group {databaseGroup.GetType().Name}.",
                CallStackFramesOffsetConstructor)
        { }
    }
}