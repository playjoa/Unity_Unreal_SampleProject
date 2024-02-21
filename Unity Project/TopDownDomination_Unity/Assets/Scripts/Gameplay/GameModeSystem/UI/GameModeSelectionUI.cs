using System.Collections.Generic;
using Gameplay.GameModeSystem.Data;
using Gameplay.UI.Base;
using GameWideSystems.GameDataBaseSystem.Controller;
using GameWideSystems.GameDataSystem.Controller;
using UnityEngine;

namespace Gameplay.GameModeSystem.UI
{
    public class GameModeSelectionUI : UIScreen
    {
        [Header("Prefab Config.")]
        [SerializeField] private GameModeOptionUI gameModeOptionUIPrefab;
        
        [Header("Rect")] 
        [SerializeField] private RectTransform gameModeOptionsRect;

        private static GameDataBaseController DataBase => GameDataBaseController.ME;
        private static GameDataController GameData => GameDataController.ME;
        private static GameModeConfigData CurrentSelectedGameMode => GameData.CurrentGameMode;
        
        private readonly List<GameModeOptionUI> _allGameModeOptions = new();
        private GameModeOptionUI _currentOptionSelected;
        
        public override void Initiate()
        {
            if (DataBase == null) return;
            
            var allGameModeOptions = DataBase.GameModesDataBase.DataItems;

            foreach (var gameModeData in allGameModeOptions)
            {
                var newGameModeOption = Instantiate(gameModeOptionUIPrefab, gameModeOptionsRect);
                newGameModeOption.Initiate(gameModeData);

                var isCurrentSelectedGameMode = gameModeData.Id.Equals(CurrentSelectedGameMode.Id);
                newGameModeOption.SetSelected(isCurrentSelectedGameMode);
                newGameModeOption.OnGameModeOptionSelected += OnMapOptionSelectedHandler;

                if (isCurrentSelectedGameMode)
                {
                    _currentOptionSelected = newGameModeOption;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var mapOption in _allGameModeOptions)
            {
                mapOption.OnGameModeOptionSelected -= OnMapOptionSelectedHandler;
            }
        }

        private void OnMapOptionSelectedHandler(GameModeOptionUI gameModeOptionUI)
        {
            if (CurrentSelectedGameMode.Id.Equals(gameModeOptionUI.DisplayingGameMode.Id)) return;
            
            _currentOptionSelected.SetSelected(false);
            _currentOptionSelected = gameModeOptionUI;
            _currentOptionSelected.SetSelected(true);
            GameData.SetGameModeData(gameModeOptionUI.DisplayingGameMode);
        }
    }
}