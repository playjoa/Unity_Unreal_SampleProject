using System;
using Gameplay.GameModeSystem.Data;
using Gameplay.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.GameModeSystem.UI
{
    public class GameModeOptionUI : MonoBehaviour
    {
        [Header("View Components")]
        [SerializeField] private Image gameModeSelectionImage;
        [SerializeField] private TextMeshProUGUI gameModeNameTMP;

        [Header("Interaction Components")] 
        [SerializeField] private Button selectionButton;

        [Header("Color Data")] 
        [SerializeField] private UIColorsData uiColorsData;

        public event Action<GameModeOptionUI> OnGameModeOptionSelected;

        public GameModeConfigData DisplayingGameMode { get; private set; }

        public void Initiate(GameModeConfigData gameModeData)
        {
            DisplayingGameMode = gameModeData;
            
            SetGameModeNameText(DisplayingGameMode);
            
            selectionButton.onClick.AddListener(OnSelectionClickHandler);
        }

        private void OnDestroy()
        {
            selectionButton.onClick.RemoveListener(OnSelectionClickHandler);
        }
        
        private void SetGameModeNameText(GameModeConfigData gameModeData)
        {
            gameModeNameTMP.text = gameModeData.GameModeName;
        }

        public void SetSelected(bool value)
        {
            gameModeSelectionImage.color = value ? uiColorsData.selectedUIColor : uiColorsData.notSelectedUIColor;
        }
        
        private void OnSelectionClickHandler()
        {
            OnGameModeOptionSelected?.Invoke(this);
        }
    }
}