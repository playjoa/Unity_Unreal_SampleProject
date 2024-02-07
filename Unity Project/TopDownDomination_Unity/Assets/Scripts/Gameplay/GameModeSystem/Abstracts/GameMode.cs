using System;
using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.Interfaces;

namespace Gameplay.GameModeSystem.Abstracts
{
    public abstract class GameMode : IGameMode
    {
        public event Action<EndGameData> OnGameEnded;
        public event Action<GameMode> OnGameTicked;
        
        public bool Active { get; protected set; }
        public int GameTime { get; protected set; }

        public abstract void InitiateGameMode();
        public abstract void EndGameMode();

        protected void InvokeGameEnd(EndGameData endGameData)
        {
            OnGameEnded?.Invoke(endGameData);
        }
        
        protected void InvokeGameTick()
        {
            OnGameTicked?.Invoke(this);
        }
    }

    public abstract class GameMode<TGameModeData> : GameMode where TGameModeData : GameModeConfigData
    {
        protected TGameModeData GameModeConfigData { get; }
        protected GameController GameController { get; }
        protected IGameEntity PlayerEntity { get; }

        protected GameMode(TGameModeData gameModeData, GameController gameController)
        {
            GameModeConfigData = gameModeData;
            GameController = gameController;
            PlayerEntity = gameController.PlayerEntity;
            GameTime = 0;
        }

        public override void InitiateGameMode()
        {
            GameController.PlayerEntity.EntityHealth.OnDied += OnPlayerDiedHandler;
            GameController.InvokeRepeating(nameof(GameTick), 1f, 1f);

            OnGameModeStart();
            Active = true;
        }

        public override void EndGameMode()
        {
            GameController.PlayerEntity.EntityHealth.OnDied -= OnPlayerDiedHandler;
            GameController.CancelInvoke(nameof(GameTick));
            
            OnGameModeEnd();
        }

        private void GameTick()
        {
            GameTime++;
            OnGameTick(GameTime);
            InvokeGameTick();
        }

        protected void InvokeGameModeEnd(EndGameData endGameData)
        {
            Active = false;
            InvokeGameModeEnd(endGameData);
        }
        
        protected virtual void OnGameModeStart() { }
        protected virtual void OnGameTick(int gameDuration) { }
        protected virtual void OnGameModeEnd() { }
        
        private void OnPlayerDiedHandler(HealthChangeData healthChangeData)
        {
            InvokeGameModeEnd
            (
                new EndGameData
                {
                    GameEndReason = GameEndReason.PlayerDied,
                    GameController = this.GameController
                }
            );
        }
    }
}