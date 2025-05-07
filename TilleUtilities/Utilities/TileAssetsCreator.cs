using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UTIRLib.Diagnostics.Exceptions;
using Object = UnityEngine.Object;

#nullable enable
namespace UTIRLib.TileUtilities
{
    public static class TileAssetsCreator
    {
        public static Object[] CreateFromAtlas(Type tileType, string textureAtlasPath)
        {
            if (textureAtlasPath.IsNullOrEmpty()) {
                throw new NullOrEmptyStringException(textureAtlasPath, nameof(textureAtlasPath));
            }

            Sprite[] sprites = LoadSpritesFromAtlas(textureAtlasPath);
            PropertyInfo tileSpriteProperty = GetTileSpriteProperty(tileType);
            var tiles = new Object[sprites.Length];
            int spritesCount = sprites.Length;
            for (int i = 0; i < spritesCount; i++) {
                tiles[i] = ScriptableObject.CreateInstance(tileType);
                tiles[i].name = sprites[i].name;
                tileSpriteProperty.SetValue(tiles[i], sprites[i]);
            }

            return tiles;
        }
        /// <exception cref="NullOrEmptyStringException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static TTile[] CreateFromAtlas<TTile>(string textureAtlasPath)
            where TTile : TileBase, IInitializable<Sprite> => CreateFromAtlas(typeof(TTile), textureAtlasPath) as TTile[] ??
            Array.Empty<TTile>();

        /// <exception cref="Exception"></exception>
        private static Sprite[] LoadSpritesFromAtlas(string textureAtlasPath)
        {
            textureAtlasPath = textureAtlasPath.Replace('\\', '/');
            Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(textureAtlasPath).
                OfType<Sprite>().
                ToArray();
            if (sprites.IsNullOrEmpty()) {
                throw new Exception($"Specified texture atlas is not correct or not exist. Path: {textureAtlasPath}");
            }

            return sprites;
        }

        private static PropertyInfo GetTileSpriteProperty(Type tileType) => tileType.GetProperty("sprite",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ?? throw new Exception("Property not found.");
    }
}
