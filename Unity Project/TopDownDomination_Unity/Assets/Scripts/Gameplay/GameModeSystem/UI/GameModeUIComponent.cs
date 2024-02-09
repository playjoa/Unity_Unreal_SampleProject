using System;
using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.UI
{
    public abstract class GameModeUIComponent<TGameMode> : GameModeUIComponent where TGameMode : IGameMode
    {
        public TGameMode CurrentGameMode { get; private set; }

        public override void Initiate(IGameMode gameMode)
        {
            if (gameMode is not TGameMode tGameMode)
            {
                ToggleUI(false);
                return;
            }

            CurrentGameMode = tGameMode;
            OnInitiate(CurrentGameMode);
        }

        protected virtual void OnInitiate(TGameMode currentGameMode)
        {
            
        }
    }

    public abstract class GameModeUIComponent : MonoBehaviour
    {
        public abstract void Initiate(IGameMode gameMode);

        private void OnDestroy()
        {
            OnCleanUp();
        }

        protected virtual void OnCleanUp()
        {
            
        }

        public void ToggleUI(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}