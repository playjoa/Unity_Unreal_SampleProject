﻿using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Timed.Data
{
    public class TimedGameModeData : GameModeConfigData
    {
        [Header("Time Trial Config.")]
        [SerializeField] private int gameModeTimeLimit;
        
        public int GameModeTimeLimit => gameModeTimeLimit;
        
        public override IGameMode GenerateGameMode(GameController gameController)
        {
            throw new System.NotImplementedException();
        }
    }
}