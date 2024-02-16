using System.Collections.Generic;
using System.Linq;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.MapLoaderSystem.Abstracts;
using Gameplay.MapLoaderSystem.Controller;
using UnityEngine;

namespace Gameplay.MapLoaderSystem.Components
{
    public class MapLoaderScreenController : MonoBehaviour
    {
        [Header("Map Loading Screen Controller")]
        [SerializeField] private MapLoaderController mapLoader;
        
        [Header("View Modules")]
        [SerializeField] private GameObject waitingForDataPanel;
        [SerializeField] private GameObject loadingPanel;
        
        [Header("View Modules")]
        [SerializeField] private List<MapLoadingScreenModule> mapLoadingScreenModules;
        
        private bool _loadingMap; 
        
        private void Awake()
        {
            mapLoader.OnStartedLoadingScreen += OnMapStartedLoadingHandler;
            mapLoader.OnFinishedLoadingScreen += OnFinishedLoadingHandler;
        }

        private void Start()
        {
            GameController.ME.OnGameSystemsInitialized += OnGameInitializedHandler;
        }

        private void OnDestroy()
        {
            mapLoader.OnStartedLoadingScreen -= OnMapStartedLoadingHandler;
            mapLoader.OnFinishedLoadingScreen -= OnFinishedLoadingHandler;
            GameController.ME.OnGameSystemsInitialized -= OnGameInitializedHandler;
        }

        public void UpdateChildModules()
        {
            if (!Application.isEditor) return;

            mapLoadingScreenModules = GetComponentsInChildren<MapLoadingScreenModule>().ToList();
        }

        private void ToggleScreen(bool valueToSet)
        {
            loadingPanel.SetActive(valueToSet);
        }

        private void LateUpdate()
        {
            if (!_loadingMap) return;
            
            foreach (var mapLoadingScreenModule in mapLoadingScreenModules)
            {
                mapLoadingScreenModule.OnUpdate(mapLoader);
            }
        }

        private void OnMapStartedLoadingHandler(MapLoaderController mapLoaderController)
        {
            _loadingMap = true;

            foreach (var mapLoadingScreenModule in mapLoadingScreenModules)
            {
                mapLoadingScreenModule.Initiate(mapLoaderController.CurrentLoadingMap);
            }

            ToggleScreen(true);
            waitingForDataPanel.SetActive(false);
        }

        private void OnFinishedLoadingHandler(MapLoaderController mapLoaderController)
        {
            _loadingMap = false;
        }
        
        private void OnGameInitializedHandler(GameController gameController)
        {
            // TODO - Add Screen Fade
            // gameController.InvokeScreenFade(0.5f, 0.75f);
            
            ToggleScreen(false);
            waitingForDataPanel.SetActive(false);
        }
    }
}