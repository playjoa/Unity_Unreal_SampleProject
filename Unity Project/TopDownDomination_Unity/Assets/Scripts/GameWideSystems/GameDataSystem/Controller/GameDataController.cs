using System;
using System.Collections;
using Gameplay.Entity.Base.Data;
using Gameplay.GameModeSystem.Data;
using Gameplay.MapLoaderSystem.Data;
using GameWideSystems.GameDataBaseSystem.Controller;
using GameWideSystems.GameInitialization.Controller;
using GameWideSystems.GameInitialization.Interfaces;
using UnityEngine;
using Utils.Patterns;

namespace GameWideSystems.GameDataSystem.Controller
{
    public class GameDataController : MonoBehaviourSingleton<GameDataController>, IGameWideSystem
    {
        [Header("Default Maps Configuration")] 
        [SerializeField] private GameModeConfigData defaultGameModeConfigData;
        [SerializeField] private MapData defaultMapData;
        
        [Header("Default Player")]
        [SerializeField] private EntityData defaultPlayerEntity;
        
        public string AppSystemName => "Game Data";
        
        public event Action<MapData> OnCurrentMapSaved;
        public event Action<GameModeConfigData> OnCurrentGameModeSaved;
        public event Action<EntityData> OnCurrentPlayerEntityDataSaved;
        
        public MapData CurrentMap { get; private set; }
        public GameModeConfigData CurrentGameMode { get; private set; }
        public EntityData CurrentPlayerEntityData { get; private set; }

        private GameDataBaseController DataBase => GameDataBaseController.ME;
        
        private readonly SaveDataController _saveDataController = new();
        
        public IEnumerator Initiate(GameInitializationController appInitializationController)
        {
            CheckForUnsavedValues();
            yield return true;
            RetrieveDataItems();
        }
        
        private void CheckForUnsavedValues()
        {
            if (!_saveDataController.HasSavedGameMode())
            {
                _saveDataController.SaveGameModeId(defaultGameModeConfigData);
            }
            
            if (!_saveDataController.HasSavedMapData())
            {
                _saveDataController.SaveMapId(defaultMapData);
            }
            
            if (!_saveDataController.HasSavedPlayerEntity())
            {
                _saveDataController.SavePlayerEntityId(defaultPlayerEntity);
            }
        }

        private void RetrieveDataItems()
        {
            if (DataBase.MapsDataBase.TryGetDataItem(_saveDataController.GetSavedMapDataId(), out var mapData))
            {
                CurrentMap = mapData;
            }
            
            if (DataBase.GameModesDataBase.TryGetDataItem(_saveDataController.GetSavedGameModeId(), out var gameModeData))
            {
                CurrentGameMode = gameModeData;
            }
            
            if (DataBase.EntitiesDataBase.TryGetDataItem(_saveDataController.GetSavedPlayerEntityId(), out var entityData))
            {
                CurrentPlayerEntityData = entityData;
            }
        }

        public void SetGameModeData(GameModeConfigData gameModeData)
        {
            if (gameModeData == null) return;
            if (CurrentGameMode.Id == gameModeData.Id) return;
            
            CurrentGameMode = gameModeData;
            _saveDataController.SaveGameModeId(gameModeData);
            OnCurrentGameModeSaved?.Invoke(gameModeData);
        }
        
        public void SetMapData(MapData mapData)
        {
            if (mapData == null) return;
            if (CurrentMap.Id == mapData.Id) return;
            
            CurrentMap = mapData;
            _saveDataController.SaveMapId(mapData);
            OnCurrentMapSaved?.Invoke(mapData);
        }
        
        public void SetPlayerEntityData(EntityData playerEntityData)
        {
            if (playerEntityData == null) return;
            if (CurrentPlayerEntityData.Id == playerEntityData.Id) return;
            if (playerEntityData.EntityType != EntityType.Player) return;
            
            CurrentPlayerEntityData = playerEntityData;
            _saveDataController.SavePlayerEntityId(playerEntityData);
            OnCurrentPlayerEntityDataSaved?.Invoke(playerEntityData);
        }
    }
}