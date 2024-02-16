﻿using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameCameraSystem.Controller;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Data;
using Gameplay.GameModeSystem.Controller;
using Gameplay.GameModeSystem.Data;
using Gameplay.MapLoaderSystem.Controller;
using Gameplay.PlayerInputs.Controller;
using Gameplay.SpawnSystem.Controller;
using Gameplay.UI.Controller;
using UnityEngine;

namespace Gameplay.GameControllerSystem.Controller
{
    public class GameController : MonoBehaviour
    {
        public static GameController ME { get; private set; }
        
        [Header("Base Systems")] 
        [SerializeField] private MapLoaderController mapLoaderController;

        [Header("Base Systems")] 
        [SerializeField] private InputsController inputsController;
        
        [Header("Game Systems")] 
        [SerializeField] private SpawnController spawnController;
        [SerializeField] private GameCameraController gameCameraController;

        [Header("Game Mode")] 
        [SerializeField] private GameModeController gameModeController;

        [Header("UI")] 
        [SerializeField] private GameUIController gameUIController;
        
        public GameData GameData { get; private set; }
        
        public MapLoaderController MapLoaderController => mapLoaderController;
        public InputsController InputsController => inputsController;
        public SpawnController SpawnController => spawnController;
        public GameCameraController GameCameraController => gameCameraController;
        public GameModeController GameModeController => gameModeController;
        
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
            GetGameData();
            SetUpSystemsInitializationOrder();

            gameModeController.OnGameModeEnded += OnGameModeEndedHandler;

            foreach (var gameSystem in _gameSystems)
            {
                yield return gameSystem.Initiate(this);
            }

            OnGameSystemsInitialized?.Invoke(this);
        }
        
        private void GetGameData()
        {
            // TODO - Get GameData
            // GameData = GameLoaderController.ME.CurrentGameData;
        }

        private void SetUpSystemsInitializationOrder()
        {
            Debug.Log("----SettingUp Initialization Order----");
            
            // Base Systems
            QueueSystemInitialization(mapLoaderController);
            QueueSystemInitialization(inputsController);

            // Game Systems
            QueueSystemInitialization(spawnController);
            QueueSystemInitialization(gameCameraController);

            // Game Mode Systems
            QueueSystemInitialization(gameModeController);

            // UI Systems
            QueueSystemInitialization(gameUIController);
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