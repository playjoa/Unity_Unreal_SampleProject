using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.Data
{
    public abstract class GameModeConfigData : ScriptableObject
    {
        public abstract IGameMode GenerateGameMode(GameController gameController);
    }
}