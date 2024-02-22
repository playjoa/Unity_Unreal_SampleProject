using System;
using System.Collections;
using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.Abstracts
{
    public abstract class GameMode : IGameMode
    {
        public event Action<EndGameData> OnGameEnded;
        public event Action<int> OnGameModeTick;
        
        public bool Active { get; private set; }
        public int GameTime { get; private set; }
        
        public GameModeConfigData BaseGameModeData { get; private set; }

        protected GameController GameController { get; private set; }
        protected IGameEntity PlayerEntity { get; private set; }

        private Coroutine _gameTickCoroutine;
        
        protected GameMode(GameModeConfigData gameModeConfigData, GameController gameController)
        {
            BaseGameModeData = gameModeConfigData;
            GameController = gameController;
            PlayerEntity = gameController.PlayerEntity;
        }
        
        public void InitiateGameMode()
        {
            GameController.PlayerEntity.EntityHealth.OnDied += OnPlayerDiedHandler;

            if (BaseGameModeData.IsTimedGameMode)
            {
                InitiateGameTime();
                _gameTickCoroutine = GameController.StartCoroutine(GameTickCoroutine());
            }
            
            OnInitiateGameMode();
            
            Active = true;
        }

        public void EndGameMode()
        {
            GameController.PlayerEntity.EntityHealth.OnDied -= OnPlayerDiedHandler;

            if (BaseGameModeData.IsTimedGameMode && _gameTickCoroutine != null)
            {
                GameController.StopCoroutine(_gameTickCoroutine);
            }

            OnEndGameMode();
        }
        
        private IEnumerator GameTickCoroutine()
        {
            var tickWait = new WaitForSeconds(1);
            yield return tickWait;
            
            while (IsInTimeLimit())
            {
                ProcessGameTime();
                OnGameTick(GameTime);
                OnGameModeTick?.Invoke(GameTime);
                yield return tickWait;
            }

            InvokeGameEnd
            (
                new EndGameData
                {
                    GameEndReason = GameEndReason.TimeOver,
                    GameController = this.GameController
                }
            );

            _gameTickCoroutine = null;
        }
        
        private void InitiateGameTime()
        {
            switch (BaseGameModeData.GameModeTimerMode)
            {
                case GameModeConfigData.TimerMode.CountToLimit:
                    GameTime = 0;
                    break;
                case GameModeConfigData.TimerMode.CountFromLimitToZero:
                    GameTime = BaseGameModeData.GameModeTimeLimit;
                    break;
            }
        }
        
        private bool IsInTimeLimit()
        {
            switch (BaseGameModeData.GameModeTimerMode)
            {
                case GameModeConfigData.TimerMode.CountToLimit:
                    return GameTime < BaseGameModeData.GameModeTimeLimit;
                case GameModeConfigData.TimerMode.CountFromLimitToZero:
                    return GameTime > 0;
                default:
                    return true;
            }
        }

        private void ProcessGameTime()
        {
            switch (BaseGameModeData.GameModeTimerMode)
            {
                case GameModeConfigData.TimerMode.CountToLimit:
                    GameTime++;
                    break;
                case GameModeConfigData.TimerMode.CountFromLimitToZero:
                    GameTime--;
                    break;
            }
        }

        protected virtual void OnInitiateGameMode() { }
        protected virtual void OnGameTick(int gameDuration) { }
        protected virtual void OnEndGameMode() { }

        protected void InvokeGameEnd(EndGameData endGameData)
        {
            Active = false;
            OnGameEnded?.Invoke(endGameData);
        }
        
        private void OnPlayerDiedHandler(HealthChangeData healthChangeData)
        {
            InvokeGameEnd
            (
                new EndGameData
                {
                    GameEndReason = GameEndReason.PlayerDied,
                    GameController = this.GameController
                }
            );
        }
    }

    public abstract class GameMode<TGameModeData> : GameMode where TGameModeData : GameModeConfigData
    {
        protected TGameModeData GameModeConfigData { get; }

        protected GameMode(TGameModeData gameModeData, GameController gameController) : base(gameModeData, gameController)
        {
            GameModeConfigData = gameModeData;
        }
    }
}