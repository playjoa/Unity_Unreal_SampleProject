using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.UI.Interfaces;
using UnityEngine;

namespace Gameplay.UI.Controller
{
    public class GameUIController : MonoBehaviour, IGameplaySystem
    {
        [Header("Target Canvases")]
        [SerializeField] private Canvas playerCanvas;
        [SerializeField] private Canvas gameCanvas;
        
        private List<IPlayerUI> _playerUIComponents = new();
        private List<IGameUI> _gameUIComponents = new();
        
        public IEnumerator Initiate(GameController gameplaySystem)
        {
            _playerUIComponents = playerCanvas.GetComponentsInChildren<IPlayerUI>(true).ToList();
            _gameUIComponents = gameCanvas.GetComponentsInChildren<IGameUI>(true).ToList();
            
            yield return true;

            foreach (var uiComponent in _playerUIComponents)
            {
                uiComponent.Initiate(gameplaySystem.PlayerEntity);
            }
            
            foreach (var uiComponent in _gameUIComponents)
            {
                uiComponent.Initiate(gameplaySystem);
            }
        }

        public void OnCleanUp()
        {
        }
        
        public void ToggleCanvas(bool value)
        {
            gameObject.SetActive(value);
        }
        
        public void ToggleUIComponents(bool valueToSet)
        {
            foreach (var uiComponent in _playerUIComponents)
            {
                uiComponent.ToggleUI(valueToSet);
            }
            
            foreach (var uiComponent in _gameUIComponents)
            {
                uiComponent.ToggleUI(valueToSet);
            }
        }
    }
}