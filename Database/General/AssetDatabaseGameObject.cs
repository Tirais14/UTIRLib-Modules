using System;
using UnityEngine;

#nullable enable
namespace UTIRLib.Database
{
    /// <summary>
    /// Contains game object (prefab) asset reference and other asset loading tools and information
    /// </summary>
    public class AssetDatabaseGameObject : AssetDatabase<AssetDatabaseItemGameObject, GameObject>
    {
        public AssetDatabaseGameObject() : base()
        { }
        public AssetDatabaseGameObject(bool isLogging) : base(isLogging)
        { }
    }
}