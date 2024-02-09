using Gameplay.GameModeSystem.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace Gameplay.GameModeSystem.UI
{
    public class GameModeTimerUIComponent : GameModeUIComponent
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI gameModeTimerTMP;

        [Header("Images")] 
        [SerializeField] private Image gamTimeSliderImage;
        
        private IGameMode _currentGameMode;
        
        public override void Initiate(IGameMode gameMode)
        {
            _currentGameMode = gameMode;
            if (!_currentGameMode.BaseGameModeData.IsTimedGameMode)
            {
                ToggleUI(false);
                return;
            }

            SetGameTimeText(_currentGameMode.GameTime);
            SetGameTimeProgress(_currentGameMode);

            _currentGameMode.OnGameModeTick += OnGameModeTickHandler;
        }

        protected override void OnCleanUp()
        {
            _currentGameMode.OnGameModeTick += OnGameModeTickHandler;
        }

        private void SetGameTimeText(float gameTime)
        {
            gameModeTimerTMP.text = gameTime.ToNiceTimer();
        }

        private void SetGameTimeProgress(IGameMode gameMode)
        {
            gamTimeSliderImage.fillAmount = gameMode.GameTime / (float) gameMode.BaseGameModeData.GameModeTimeLimit;
        }

        private void OnGameModeTickHandler(int gameTime)
        {
            SetGameTimeText(gameTime);
            SetGameTimeProgress(_currentGameMode);
        }
    }
}