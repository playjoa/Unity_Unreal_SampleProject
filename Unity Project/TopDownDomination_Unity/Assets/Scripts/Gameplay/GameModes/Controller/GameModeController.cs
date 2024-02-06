using System;
using System.Collections;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModes.Base;
using Gameplay.GameModes.Data;
using UnityEngine;

namespace Gameplay.GameModes.Controller
{
    public class GameModeController : MonoBehaviour, IGameplaySystem
    {
        public event Action<EndGameData> OnGameModeEnded;
                
        private bool _initiated = false;

        private IGameMode _currentGameMode;

        public IEnumerator Initiate(GameController gameplaySystem)
        {
            yield return true;
            
            _currentGameMode.OnGameModeEnd += OnCurrentGameModeEndedHandler;
            _currentGameMode.InitiateGameMode();

            _initiated = true;
        }

        public void OnEnd()
        {
            _currentGameMode.EndGameMode();
        }

        // TODO - Create game modes
        private IGameMode GenerateGameMode()
        {
            return default;
        }

        private void OnCurrentGameModeEndedHandler(EndGameData endGameData)
        {
            OnGameModeEnded?.Invoke(endGameData);
        }
    }
}