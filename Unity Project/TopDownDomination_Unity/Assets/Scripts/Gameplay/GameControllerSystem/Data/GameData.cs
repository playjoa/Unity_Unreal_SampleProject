using Gameplay.GameModeSystem.Data;
using Gameplay.MapLoaderSystem.Data;

namespace Gameplay.GameControllerSystem.Data
{
    public class GameData
    {
        public MapData MapData { get; private set; }
        public GameModeConfigData GameModeData { get; private set; }
        
        public GameData(MapData mapData, GameModeConfigData gameModeData)
        {
            MapData = mapData;
            GameModeData = gameModeData;
        }
    }
}