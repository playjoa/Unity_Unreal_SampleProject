using System;
using Gameplay.GameModeSystem.Data;

namespace Gameplay.GameModeSystem.Interfaces
{
    public interface IGameMode
    {
        event Action<EndGameData> OnGameEnded;
        event Action<int> OnGameModeTick;
        
        bool Active { get; }
        int GameTime { get; }

        GameModeConfigData BaseGameModeData { get; }

        void InitiateGameMode();
        void EndGameMode();
    }
}