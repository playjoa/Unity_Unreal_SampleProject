using System;
using System.Collections;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Interfaces;
using Gameplay.GameModeSystem.Data;
using UnityEngine;

namespace Gameplay.GameModeSystem.Controller
{
    public class GameModeController : MonoBehaviour, IGameplaySystem
    {
        public event Action<EndGameData> OnGameModeEnded;
                
        public IGameMode CurrentGameMode { get; private set; }
        
        private bool _initiated = false;
        
        public IEnumerator Initiate(GameController gameplaySystem)
        {
            yield return true;
            
            CurrentGameMode = gameplaySystem.GameData.GameModeData.GenerateGameMode(gameplaySystem);
            CurrentGameMode.OnGameEnded += OnCurrentGameModeEndedHandler;
            CurrentGameMode.InitiateGameMode();

            _initiated = true;
        }

        public void OnCleanUp()
        {
            
        }

        private void OnCurrentGameModeEndedHandler(EndGameData endGameData)
        {
            CurrentGameMode.EndGameMode();
            CurrentGameMode.OnGameEnded -= OnCurrentGameModeEndedHandler;
            OnGameModeEnded?.Invoke(endGameData);
        }
    }
}