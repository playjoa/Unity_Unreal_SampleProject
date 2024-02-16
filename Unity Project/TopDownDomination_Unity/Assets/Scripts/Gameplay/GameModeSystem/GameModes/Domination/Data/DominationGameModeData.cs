using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.GameModes.Domination.Controller;
using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Data
{
    [CreateAssetMenu(menuName = "GameSystems/GameModes/DominationGameModeData", fileName = "New DominationGameModeData")]
    public class DominationGameModeData : GameModeConfigData
    {
        [Header("Time Limit")] 
        [SerializeField] private int gameTimeLimit = 300;
        
        [Header("Domination Score")]
        [SerializeField] private int scoreTarget = 100;
        [SerializeField] private int scoreTickPerFlag = 1;
        
        public int GameTimeLimit => gameTimeLimit;
        public int ScoreTarget => scoreTarget;
        public int ScoreTickPerFlag => scoreTickPerFlag;
        
        public override IGameMode GenerateGameMode(GameController gameController)
        {
            return new DominationGameMode(this, gameController);
        }
    }
}