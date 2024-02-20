using System.Collections.Generic;
using Gameplay.MapLoaderSystem.Data;
using Gameplay.UI.Base;
using GameWideSystems.GameDataBaseSystem.Controller;
using GameWideSystems.GameDataSystem.Controller;
using UnityEngine;

namespace Gameplay.MapLoaderSystem.Components
{
    public class MapSelectionControllerUI : UIScreen
    {
        [Header("Prefab Config.")]
        [SerializeField] private MapOptionUI mapOptionUIPrefab;

        [Header("Rect")] 
        [SerializeField] private RectTransform mapOptionsRect;

        private static GameDataBaseController DataBase => GameDataBaseController.ME;
        private static GameDataController GameData => GameDataController.ME;
        private static MapData CurrentSelectedMap => GameData.CurrentMap;
        
        private readonly List<MapOptionUI> _allMapOptions = new();
        private MapOptionUI _currentOptionSelected;
        
        public override void Initiate()
        {
            var allMapOptions = DataBase.MapsDataBase.DataItems;

            foreach (var mapData in allMapOptions)
            {
                var newMapOption = Instantiate(mapOptionUIPrefab, mapOptionsRect);
                newMapOption.Initiate(mapData);

                var isCurrentSelectedMap = mapData.Id.Equals(CurrentSelectedMap.Id);
                newMapOption.SetSelected(isCurrentSelectedMap);
                newMapOption.OnMapOptionSelected += OnMapOptionSelectedHandler;

                if (isCurrentSelectedMap)
                {
                    _currentOptionSelected = newMapOption;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var mapOption in _allMapOptions)
            {
                mapOption.OnMapOptionSelected -= OnMapOptionSelectedHandler;
            }
        }

        private void OnMapOptionSelectedHandler(MapOptionUI mapOptionUI)
        {
            if (CurrentSelectedMap.Id.Equals(mapOptionUI.DisplayingMapData.Id)) return;
            
            _currentOptionSelected.SetSelected(false);
            _currentOptionSelected = mapOptionUI;
            _currentOptionSelected.SetSelected(true);
            GameData.SetMapData(mapOptionUI.DisplayingMapData);
        }
    }
}