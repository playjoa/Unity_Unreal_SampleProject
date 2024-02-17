using System.Collections;
using System.Collections.Generic;
using Gameplay.Entity.Base.Data;
using Gameplay.GameModeSystem.Data;
using Gameplay.MapLoaderSystem.Data;
using GameWideSystems.GameDataBaseSystem.Abstracts;
using GameWideSystems.GameDataBaseSystem.Data;
using GameWideSystems.GameDataBaseSystem.Interfaces;
using GameWideSystems.GameInitialization.Controller;
using GameWideSystems.GameInitialization.Interfaces;
using UnityEngine;
using Utils.Pattern;

namespace GameWideSystems.GameDataBaseSystem.Controller
{
    public class GameDataBaseController : MonoBehaviourSingleton<GameDataBaseController>, IGameWideSystem
    {
        [Header("Target DataBase")]
        [SerializeField] private GameDataBase gameDataBase;
        
        public string AppSystemName => "Database";
        private readonly List<IDataBase> _allDataBaseHolders = new();
        
        public ItemDataBaseHolder<EntityData> EntitiesDataBase { get; private set; }
        public ItemDataBaseHolder<MapData> MapsDataBase { get; private set; }
        public ItemDataBaseHolder<GameModeConfigData> GameModesDataBase { get; private set; }
        
        public IEnumerator Initiate(GameInitializationController appInitializationController)
        {
            BuildDataBases(gameDataBase);
            yield return true;
        }
        
        private void BuildDataBases(GameDataBase dataBase)
        {
            EntitiesDataBase = new ItemDataBaseHolder<EntityData>(dataBase.EntitiesDataBaseList);
            MapsDataBase = new ItemDataBaseHolder<MapData>(dataBase.MapsDataBaseList);
            GameModesDataBase = new ItemDataBaseHolder<GameModeConfigData>(dataBase.GameModesDataBaseList);

            _allDataBaseHolders.Add(EntitiesDataBase);
            _allDataBaseHolders.Add(MapsDataBase);
            _allDataBaseHolders.Add(GameModesDataBase);
            
            foreach (var dataBaseHolder in _allDataBaseHolders)
            {
                dataBaseHolder.InitiateDataBase();
            }
        }
    }
}