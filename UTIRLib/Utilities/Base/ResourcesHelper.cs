using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UTIRLib.Diagnostics;

namespace UTIRLib
{
    public static class ResourcesHelper
    {
        /// <summary>
        /// Load all assets with specified type. Also search in subfolders
        /// </summary>
        public static T[] LoadAll<T>(string fullPath) where T : UnityEngine.Object
        {
            if (string.IsNullOrEmpty(fullPath)) { Debug.LogError(new NullOrEmptyStringMessage(nameof(fullPath))); return Array.Empty<T>(); }

            List<T> loadedObjectsList = new();
            T[] loadedObjects = Resources.LoadAll<T>(fullPath);
            if (loadedObjects != null && loadedObjects.Length > 0) { loadedObjectsList.AddRange(loadedObjects); }

            string[] childDirectories = Directory.GetDirectories(fullPath, "*", SearchOption.AllDirectories);
            for (int i = 0; i < childDirectories.Length; i++) {
                loadedObjects = Resources.LoadAll<T>(childDirectories[i]);
                if (loadedObjects != null && loadedObjects.Length > 0) { loadedObjectsList.AddRange(loadedObjects); }
            }

            return loadedObjectsList.ToArray();
        }
    }
}
