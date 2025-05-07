using System;
using Newtonsoft.Json;
using UnityEngine;

namespace UTIRLib.Json
{
    [Serializable]
    [JsonObject]
    internal struct SpriteData
    {
        public string SpriteName { get; set; }
        public string SpritePath { get; set; }
    }
}
