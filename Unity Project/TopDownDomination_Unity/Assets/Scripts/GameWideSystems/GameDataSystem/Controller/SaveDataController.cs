using Gameplay.Entity.Base.Data;
using Gameplay.GameModeSystem.Data;
using Gameplay.MapLoaderSystem.Data;
using UnityEngine;

namespace GameWideSystems.GameDataSystem.Controller
{
    public class SaveDataController
    {
        private const string SELECTED_MAP_ID_KEY = "SELECTED_MAP_ID_KEY";
        private const string SELECTED_GAMEMODE_ID_KEY = "SELECTED_GAMEMODE_ID_KEY";
        private const string SELECTED_PLAYER_ENTITY_ID_KEY = "SELECTED_PLAYER_ENTITY_ID_KEY";

        public bool HasSavedMapData()
        {
            return PlayerPrefs.HasKey(SELECTED_MAP_ID_KEY);
        }

        public string GetSavedMapDataId()
        {
            if (!HasSavedMapData())
                return string.Empty;
            
            return PlayerPrefs.GetString(SELECTED_MAP_ID_KEY);
        }
        
        public void SaveMapId(MapData mapData)
        {
            PlayerPrefs.SetString(SELECTED_MAP_ID_KEY, mapData.Id);
        }
        
        public bool HasSavedGameMode()
        {
            return PlayerPrefs.HasKey(SELECTED_GAMEMODE_ID_KEY);
        }

        public string GetSavedGameModeId()
        {
            if (!HasSavedMapData())
                return string.Empty;
            
            return PlayerPrefs.GetString(SELECTED_GAMEMODE_ID_KEY);
        }
        
        public void SaveGameModeId(GameModeConfigData gameModeConfigData)
        {
            PlayerPrefs.SetString(SELECTED_GAMEMODE_ID_KEY, gameModeConfigData.Id);
        }
        
        public bool HasSavedPlayerEntity()
        {
            return PlayerPrefs.HasKey(SELECTED_PLAYER_ENTITY_ID_KEY);
        }

        public string GetSavedPlayerEntityId()
        {
            if (!HasSavedMapData())
                return string.Empty;
            
            return PlayerPrefs.GetString(SELECTED_PLAYER_ENTITY_ID_KEY);
        }
        
        public void SavePlayerEntityId(EntityData entityData)
        {
            if (entityData.EntityType != EntityType.Player) return;
            
            PlayerPrefs.SetString(SELECTED_PLAYER_ENTITY_ID_KEY, entityData.Id);
        }
    }
}