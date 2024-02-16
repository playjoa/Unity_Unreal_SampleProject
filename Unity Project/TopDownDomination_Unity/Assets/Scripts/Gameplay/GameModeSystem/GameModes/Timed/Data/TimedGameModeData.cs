using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.GameModes.Timed.Controller;
using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Timed.Data
{
    [CreateAssetMenu(menuName = "GameSystems/GameModes/TimedGameModeData", fileName = "New TimedGameModeData")]
    public class TimedGameModeData : GameModeConfigData
    {
        public override IGameMode GenerateGameMode(GameController gameController)
        {
            return new TimedGameMode(this, gameController);
        }
    }
}