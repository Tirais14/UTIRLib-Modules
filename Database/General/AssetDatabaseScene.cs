using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UTIRLib.Database.Structs;

#nullable enable
namespace UTIRLib.Database
{

    public class AssetDatabaseScene : Database<string, AssetDatabaseItemScene>
    {
        public AssetDatabaseScene() : base()
        { }
        public AssetDatabaseScene(bool isLogging) : base(isLogging)
        { }

        public async Task<SceneInstance> LoadSceneAsync(LoadSceneInfo loadSceneInfo)
        {
            AssetDatabaseItemScene? databaseItem = GetValue(loadSceneInfo.sceneName);
            if (databaseItem == null) {
                return default;
            }

            return await databaseItem.LoadSceneAsync(loadSceneInfo);
        }

        /// <returns>Null if scene with specified name not loaded</returns>
        public SceneInstance GetScene(string sceneName)
        {
            AssetDatabaseItemScene? databaseItem = GetValue(sceneName);
            if (databaseItem?.IsAssetLoaded ?? false) { 
                return databaseItem.Asset;
            }
            else return default;
        }
    }
}