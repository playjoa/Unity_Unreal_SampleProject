using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;
using Utils.UniqueId.Components;

namespace Gameplay.GameModeSystem.Data
{
    public abstract class GameModeConfigData : ScriptableObjectWithId
    {
        [Header("Timer Configuration")]
        [SerializeField] private bool isTimedGameMode = true;
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