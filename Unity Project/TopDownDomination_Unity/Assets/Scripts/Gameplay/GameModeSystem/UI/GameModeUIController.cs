using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Interfaces;
using Gameplay.UI.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.UI
{
    public class GameModeUIController : MonoBehaviour, IGameUI
    {
        [Header("UI Components")]
        [SerializeField] private GameModeUIComponent[] gameModeUIComponents;
        
        private IGameMode _currentGameMode;
        
        public void Initiate(GameController gameController)
        {
            _currentGameMode = gameController.GameModeController.CurrentGameMode;

            foreach (var uiComponent in gameModeUIComponents)
            {
                uiComponent.Initiate(_currentGameMode);
            }
        }

        public void ToggleUI(bool valueToSet)
        {
            gameObject.SetActive(false);
        }

        public void UpdateChildModules()
        {
            if (!Application.isEditor) return;

            gameModeUIComponents = GetComponentsInChildren<GameModeUIComponent>();
        }
    }
}