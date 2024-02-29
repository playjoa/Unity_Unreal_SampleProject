using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.UI
{
    public abstract class GameUIComponent : MonoBehaviour
    {
        protected IGameMode CurrentGameMode { get; private set; }

        public void Initiate(IGameMode gameMode)
        {
            CurrentGameMode = gameMode;
            OnInitiated(CurrentGameMode);
        }

        protected abstract void OnInitiated(IGameMode gameMode);

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