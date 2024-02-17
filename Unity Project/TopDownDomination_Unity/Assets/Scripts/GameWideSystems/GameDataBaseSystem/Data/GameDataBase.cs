using System.Collections.Generic;
using Gameplay.Entity.Base.Data;
using Gameplay.GameModeSystem.Data;
using Gameplay.MapLoaderSystem.Data;
using UnityEngine;

namespace GameWideSystems.GameDataBaseSystem.Data
{
    [CreateAssetMenu(menuName = "GameSystems/Data/GameDataBase", fileName = "GameDataBase")]
    public class GameDataBase : ScriptableObject
    {
        [Header("Entities DataBase")]
        [SerializeField] private List<EntityData> entitiesDataBaseList = new();
        
        [Header("Maps DataBase")]
        [SerializeField] private List<MapData> mapsDataBaseList = new();
        
        [Header("GameModes DataBase")]
        [SerializeField] private List<GameModeConfigData> gameModesDataBaseList = new();
        
        public List<MapData> MapsDataBaseList => mapsDataBaseList;
        public List<EntityData> EntitiesDataBaseList => entitiesDataBaseList;
        public List<GameModeConfigData> GameModesDataBaseList => gameModesDataBaseList;
        
#if UNITY_EDITOR
        public void UpdateEntitiesDataList(List<EntityData> entitiesDataList)
        {
            var previousCount = entitiesDataBaseList.Count;
            
            entitiesDataBaseList = entitiesDataList;
            
            Debug.Log($"Updated Entities List! \n " +
                      $"Previous Count: {previousCount} New Count: {entitiesDataList.Count}");
        }
        
        public void UpdateMapsDataList(List<MapData> mapsDataList)
        {
            var previousCount = mapsDataBaseList.Count;
            
            mapsDataBaseList = mapsDataList;
            
            Debug.Log($"Updated Maps List! \n " +
                      $"Previous Count: {previousCount} New Count: {mapsDataList.Count}");
        }
        
        public void UpdateGameModesDataList(List<GameModeConfigData> gameModesDataList)
        {
            var previousCount = gameModesDataBaseList.Count;
            
            gameModesDataBaseList = gameModesDataList;
            
            Debug.Log($"Updated GameModes List! \n " +
                      $"Previous Count: {previousCount} New Count: {gameModesDataList.Count}");
        }
#endif
    }
}