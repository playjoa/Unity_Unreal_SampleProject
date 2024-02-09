using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.Data
{
    public abstract class GameModeConfigData : ScriptableObject
    {
        [Header("Timer Configuration")]
        [SerializeField] private bool isTimedGameMode;
        [SerializeField] private int gameModeTimeLimit = 300;
        [SerializeField] private TimerMode gameModeTimerMode = TimerMode.CountToLimit;
        
        public bool IsTimedGameMode => isTimedGameMode;
        public int GameModeTimeLimit => gameModeTimeLimit;
        public TimerMode GameModeTimerMode => gameModeTimerMode;
        
        public abstract IGameMode GenerateGameMode(GameController gameController);
        
        public enum TimerMode
        {
            CountToLimit,
            CountFromLimitToZero
        }
    }
}