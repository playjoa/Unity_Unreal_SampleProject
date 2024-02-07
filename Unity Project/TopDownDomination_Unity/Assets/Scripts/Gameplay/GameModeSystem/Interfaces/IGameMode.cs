using System;
using Gameplay.GameModeSystem.Data;

namespace Gameplay.GameModeSystem.Interfaces
{
    public interface IGameMode
    {
        event Action<EndGameData> OnGameEnded;
        
        bool Active { get; }
        int GameTime { get; }
        
        void InitiateGameMode();
        void EndGameMode();
    }
}