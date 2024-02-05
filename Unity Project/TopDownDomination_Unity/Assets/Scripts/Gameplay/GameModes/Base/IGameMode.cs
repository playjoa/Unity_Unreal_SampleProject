using System;
using Gameplay.GameModes.Data;

namespace Gameplay.GameModes.Base
{
    public interface IGameMode
    {
        event Action<EndGameData> OnGameModeEnd;
        void InitiateGameMode();
        void EndGameMode();
    }
}