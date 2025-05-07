using System.Collections.Generic;

#nullable enable
namespace UTIRLib.Enums
{
    public static class AssetFileExtensionExtensions
    {
        private readonly static Dictionary<FileExtension, string> cached = new();

        public static string ToStringWithDot(this FileExtension assetFileExtension)
        {
            if (cached.TryGetValue(assetFileExtension, out string extensionWithDot)) {
                return extensionWithDot;
            }

            extensionWithDot = '.' + assetFileExtension.ToString();
            cached.Add(assetFileExtension, extensionWithDot);
            return extensionWithDot;
        }
    }
}
