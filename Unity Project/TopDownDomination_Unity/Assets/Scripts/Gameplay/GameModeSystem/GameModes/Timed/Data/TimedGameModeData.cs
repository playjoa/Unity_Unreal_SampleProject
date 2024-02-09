using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.GameModes.Timed.Controller;
using Gameplay.GameModeSystem.Interfaces;

namespace Gameplay.GameModeSystem.GameModes.Timed.Data
{
    public class TimedGameModeData : GameModeConfigData
    {
        public override IGameMode GenerateGameMode(GameController gameController)
        {
            return new TimedGameMode(this, gameController);
        }
    }
}