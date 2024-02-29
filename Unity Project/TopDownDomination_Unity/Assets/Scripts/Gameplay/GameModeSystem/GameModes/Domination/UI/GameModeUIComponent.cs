using Gameplay.GameModeSystem.Interfaces;
using Gameplay.GameModeSystem.UI;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    public abstract class GameModeUIComponent<TGameMode> : GameUIComponent where TGameMode : IGameMode
    {
        public TGameMode CurrentGameMode { get; private set; }

        protected override void OnInitiated(IGameMode gameMode)
        {
            if (gameMode is not TGameMode tGameMode)
            {
                ToggleUI(false);
                return;
            }

            CurrentGameMode = tGameMode;
            OnInitiate(CurrentGameMode);
            ToggleUI(true);
        }

        protected virtual void OnInitiate(TGameMode currentGameMode)
        {
            
        }
    }
}