using System.Collections.Generic;
using System.Linq;
using Gameplay.Entity.Base.Data;
using Gameplay.GameModeSystem.Data;
using Gameplay.MapLoaderSystem.Data;
using GameWideSystems.GameDataBaseSystem.Data;
using UnityEditor;
using UnityEngine;

namespace GameWideSystems.GameDataBaseSystem.Editor
{
    public static class GameDataBaseEditorUtils
    {
        private const string SyncGameInventoryInfo = "DevTools/Data/Sync GameDataBase";
        private const string GameInventoryPrefabName = "GameDataBase";

        [MenuItem(SyncGameInventoryInfo)]
        public static void UpdateGameInventoryDataBase()
        {
            var gameInventoryDataBaseAssets = AssetDatabase.FindAssets($"t:{GameInventoryPrefabName}");

            if (!gameInventoryDataBaseAssets.Any())
            {
                Debug.LogError($"Could not find prefab for {GameInventoryPrefabName}");
                return;
            }
            
            if (!TryGetGameInventoryDataBaseFromGuid(gameInventoryDataBaseAssets[0], out var gameInventoryDataBase))
            {
                Debug.LogError($"Could not get {GameInventoryPrefabName} from Guid: {gameInventoryDataBaseAssets[0]}");
                return;
            }

            var allEffectsInProject = GetAllScriptableInProjectList<MapData>();
            var allWeaponDataInProject = GetAllScriptableInProjectList<EntityData>();
            var allGameModesDataInProject = GetAllScriptableInProjectList<GameModeConfigData>();

            gameInventoryDataBase.UpdateMapsDataList(allEffectsInProject);
            gameInventoryDataBase.UpdateEntitiesDataList(allWeaponDataInProject);
            gameInventoryDataBase.UpdateGameModesDataList(allGameModesDataInProject);
            
            EditorUtility.SetDirty(gameInventoryDataBase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static bool TryGetGameInventoryDataBaseFromGuid(string guid, out GameDataBase gameInventoryController)
        {
            gameInventoryController = null;

            var holderPath = AssetDatabase.GUIDToAssetPath(guid);
            gameInventoryController = AssetDatabase.LoadAssetAtPath<GameDataBase>(holderPath);

            return gameInventoryController != null;
        }
        
        private static List<TScriptable> GetAllScriptableInProjectList<TScriptable>() where TScriptable : ScriptableObject
        {
            var allScriptables = new List<TScriptable>();
            var scriptablesGuids = AssetDatabase.FindAssets($"t:{typeof(TScriptable).FullName}");
            
            foreach (var scriptableGuid in scriptablesGuids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(scriptableGuid);
                var dataObject = AssetDatabase.LoadAssetAtPath<TScriptable>(assetPath);

                if (dataObject == null) continue;
                allScriptables.Add(dataObject);
            }

            return allScriptables;
        }
    }
}