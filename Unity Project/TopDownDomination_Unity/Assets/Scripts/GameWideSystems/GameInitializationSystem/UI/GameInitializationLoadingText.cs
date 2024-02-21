using GameWideSystems.GameInitialization.Controller;
using GameWideSystems.GameInitialization.Interfaces;
using TMPro;
using UnityEngine;

namespace GameWideSystems.GameInitializationSystem.UI
{
    public class GameInitializationLoadingText : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI loadingTMP;
        
        private void Awake()
        {
            GameInitializationController.OnGameSystemStartedLoading += OnStartedLoadingSystemHandler;
        }

        private void OnDestroy()
        {
            GameInitializationController.OnGameSystemStartedLoading -= OnStartedLoadingSystemHandler;
        }
        
        private void OnStartedLoadingSystemHandler(IGameWideSystem loadingSystem)
        {
            loadingTMP.text = $"Loading {loadingSystem.AppSystemName}...";
        }
    }
}