using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.Interfaces;
using Gameplay.GameModeSystem.UI;
using Gameplay.GameModeSystem.Utils;
using Gameplay.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    public class EndGameScreenUI : GameUIComponent
    {
        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI endGameTitleTMP;
        [SerializeField] private TextMeshProUGUI endGameSubTitleTMP;

        [Header("Images")] 
        [SerializeField] private Image backGroundImage;

        [Header("Data")] 
        [SerializeField] private UIColorsData colorData;

        private const string YOU_WIN_TEXT = "You Won!";
        private const string GAME_OVER_TEXT = "Game Over!";

        protected override void OnInitiated(IGameMode gameMode)
        {
            gameMode.OnGameEnded += OnGameEndedHandler;
            gameObject.SetActive(false);
        }

        protected override void OnCleanUp()
        {
            if (CurrentGameMode == null) return;
            
            CurrentGameMode.OnGameEnded -= OnGameEndedHandler;
        }

        private void OnGameEndedHandler(EndGameData endGameData)
        {
            SetEndGameTexts(endGameData);
            gameObject.SetActive(true);
        }

        private void SetEndGameTexts(EndGameData endGameData)
        {
            var wonGame = endGameData.GameEndReason == GameEndReason.GameModeSuccess;
            
            endGameTitleTMP.color = wonGame ? colorData.titleSuccessColor : colorData.titleGameOverColor;
            endGameTitleTMP.text = wonGame ? YOU_WIN_TEXT : GAME_OVER_TEXT;
            endGameSubTitleTMP.text = endGameData.GameEndReason.GetEndGameSubTitle();
        }
        
        private void SetImages(EndGameData endGameData)
        {
            var wonGame = endGameData.GameEndReason == GameEndReason.GameModeSuccess;
            backGroundImage.color = wonGame ? colorData.backgroundSuccessColor : colorData.backgroundGameOverColor;
        }
    }
}