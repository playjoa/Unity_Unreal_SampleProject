using Gameplay.GameModeSystem.Data;

namespace Gameplay.GameControllerSystem.Data
{
    public class GameData
    {
        public GameModeConfigData GameModeData { get; private set; }
        
        public GameData(GameModeConfigData gameModeData)
        {
            GameModeData = gameModeData;
        }
    }
}