using System;
using System.Collections;
using System.Collections.Generic;
using GameWideSystems.GameDataBaseSystem.Controller;
using GameWideSystems.GameDataSystem.Controller;
using GameWideSystems.GameInitialization.Interfaces;
using UnityEngine;
using Utils.Pattern;

namespace GameWideSystems.GameInitialization.Controller
{
    public class GameInitializationController : MonoBehaviourSingleton<GameInitializationController>
    {
        [Header("Base Systems")] 
        [SerializeField] private GameDataBaseController gameDataBaseController;
        [SerializeField] private GameDataController gameDataController;

        public static event Action<GameInitializationController> OnGameInitiated;
        public static event Action<IGameWideSystem> OnGameSystemStartedLoading;
        
        private readonly List<IGameWideSystem> _appWideSystems = new();
        private IGameWideSystem _currentSystemToInitiate;
        
        private void Start()
        {            
            SetUpSystemsInitializationOrder();
            StartCoroutine(InitializationCoroutine(_appWideSystems));
        }
        
        private IEnumerator InitializationCoroutine(List<IGameWideSystem> appWideSystems)
        {
            yield return new WaitForEndOfFrame();

            foreach (var gameSystem in appWideSystems)
            {
                OnGameSystemStartedLoading?.Invoke(gameSystem);
                yield return gameSystem.Initiate(this);
            }

            OnGameInitiated?.Invoke(this);
        }
        
        public static void QuitGameApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        
        private void SetUpSystemsInitializationOrder()
        {
            // Base Systems
            QueueSystemInitialization(gameDataBaseController);
            QueueSystemInitialization(gameDataController);
        }

        private void QueueSystemInitialization(IGameWideSystem systemToInitialize)
        {
            _appWideSystems.Add(systemToInitialize);
        }
    }
}