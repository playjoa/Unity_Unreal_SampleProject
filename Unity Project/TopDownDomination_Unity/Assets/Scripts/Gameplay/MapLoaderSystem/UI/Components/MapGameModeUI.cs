using Gameplay.GameModeSystem.Data;
using Gameplay.MapLoaderSystem.Data;
using GameWideSystems.GameDataSystem.Controller;
using TMPro;
using UnityEngine;

namespace Gameplay.MapLoaderSystem.Components
{
    public class MapGameModeUI : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI gameDescTMP;

        private GameDataController GameData => GameDataController.ME;

        private GameModeConfigData CurrentGameMode => GameData.CurrentGameMode;
        private MapData CurrentMapData => GameData.CurrentMap;
        
        private void Awake()
        {
            if (GameData == null) return;
            
            SetGameDescription(CurrentMapData.MapName, CurrentGameMode.GameModeName);

            GameData.OnCurrentMapSaved += OnMapUpdatedHandler;
            GameData.OnCurrentGameModeSaved += OnGameModeUpdatedHandler;
        }

        private void OnDestroy()
        {
            if (GameData == null) return;
            
            GameData.OnCurrentMapSaved -= OnMapUpdatedHandler;
            GameData.OnCurrentGameModeSaved -= OnGameModeUpdatedHandler;
        }

        private void SetGameDescription(string mapName, string gameMode)
        {
            gameDescTMP.text = $"{mapName} - {gameMode}";
        }
        
        private void OnMapUpdatedHandler(MapData newMap)
        {
            SetGameDescription(newMap.MapName, CurrentGameMode.GameModeName);
        }
        
        private void OnGameModeUpdatedHandler(GameModeConfigData newGameMode)
        {
            SetGameDescription(CurrentMapData.MapName, newGameMode.GameModeName);
        }
    }
}