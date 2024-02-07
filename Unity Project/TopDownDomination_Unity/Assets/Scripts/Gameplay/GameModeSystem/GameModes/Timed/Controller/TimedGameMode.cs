using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Abstracts;
using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.GameModes.Timed.Data;

namespace Gameplay.GameModeSystem.GameModes.Timed.Controller
{
    public class TimedGameMode : GameMode<TimedGameModeData>
    {

        protected TimedGameMode(TimedGameModeData gameModeData, GameController gameController) : base(gameModeData, gameController)
        {
        }

        protected override void OnGameTick(int gameDuration)
        {
            if (GameTime >= GameModeConfigData.GameModeTimeLimit)
            {
                InvokeGameModeEnd
                (
                    new EndGameData
                    {
                        GameEndReason = GameEndReason.GameModeSuccess,
                        GameController = this.GameController
                    }
                );
            }
        }
    }
}