using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameCameraSystem.Controller;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameModes.Controller;
using Gameplay.GameModes.Data;
using Gameplay.PlayerInputs.Controller;
using Gameplay.SpawnSystem.Controller;
using UnityEngine;

namespace Gameplay.GameControllerSystem.Controller
{
    public class GameController : MonoBehaviour
    {
        public static GameController ME { get; private set; }
        
        [Header("Base Systems")] 
        [SerializeField] private InputsController inputsController;
        
        [Header("Game Systems")] 
        [SerializeField] private SpawnController spawnController;
        [SerializeField] private GameCameraController gameCameraController;

        [Header("Game Mode")] 
        [SerializeField] private GameModeController gameModeController;
        
        public IGameEntity PlayerEntity { get; private set; }
        
        private readonly List<IGameplaySystem> _gameSystems = new();
        public event Action<GameController> OnGameSystemsInitialized;
        
        private void Awake()
        {
            if (ME != null)
            {
                Destroy(gameObject);
                return;
            }

            ME = this;
        }
        
        private void Start()
        {
            StartCoroutine(InitializationCoroutine());
        }
        
        private IEnumerator InitializationCoroutine()
        {
            SetUpSystemsInitializationOrder();

            gameModeController.OnGameModeEnded += OnGameModeEndedHandler;

            foreach (var gameSystem in _gameSystems)
            {
                yield return gameSystem.Initiate(this);
            }

            OnGameSystemsInitialized?.Invoke(this);
        }

        private void SetUpSystemsInitializationOrder()
        {
            Debug.Log("----SettingUp Initialization Order----");
            
            // Base Systems
            QueueSystemInitialization(inputsController);

            // Game Systems
            QueueSystemInitialization(spawnController);
            QueueSystemInitialization(gameCameraController);

            // Game Mode Systems
            QueueSystemInitialization(gameModeController);

            // UI Systems
            // QueueSystemInitialization(playerGameUIController);
        }
        
        private void QueueSystemInitialization(IGameplaySystem gameSystem)
        {
            _gameSystems.Add(gameSystem);
        }
        
        private void OnGameModeEndedHandler(EndGameData endGameData)
        {
        }
    }
}