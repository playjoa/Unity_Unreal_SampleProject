using System;
using System.Collections.Generic;
using Gameplay.Entity.Base.Data;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.Abstracts;
using Gameplay.GameModeSystem.Data;
using Gameplay.GameModeSystem.GameModes.Domination.Components;
using Gameplay.GameModeSystem.GameModes.Domination.Data;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Controller
{
    public class DominationGameMode : GameMode<DominationGameModeData>
    {
        public List<DominationZone> AllDominationZones { get; private set; }

        public event Action<DominationGameMode> OnScoreUpdated;

        public int DominationPoints { get; private set; }

        public DominationGameMode(DominationGameModeData gameModeData, GameController gameController) : base(gameModeData, gameController)
        {
        }

        protected override void OnInitiateGameMode()
        {
            GameController.MapContentController.DominationController.Initiate();
            AllDominationZones = GameController.MapContentController.DominationController.DominationZones;
        }

        protected override void OnGameTick(int gameDuration)
        {
            var pointsToCount = 0;
            foreach (var dominationZone in AllDominationZones)
            {
                if (dominationZone.EntityOwnerType == EntityType.Player)
                {
                    pointsToCount += GameModeConfigData.ScoreTickPerFlag;
                }
            }

            AddDominationPoints(pointsToCount);

            if (ProcessDominationPoints())
            {
                TriggerGameEnd(GameEndReason.GameModeSuccess);
            }
        }

        private void AddDominationPoints(int pointsToAdd)
        {
            if (pointsToAdd == 0) return;

            DominationPoints = Mathf.Clamp(DominationPoints + pointsToAdd, 0, GameModeConfigData.ScoreTarget);
            OnScoreUpdated?.Invoke(this);
        }

        private bool ProcessDominationPoints()
        {
            if (DominationPoints >= GameModeConfigData.ScoreTarget)
                return true;

            return false;
        }

        private void TriggerGameEnd(GameEndReason endReason)
        {
            InvokeGameEnd
            (
                new EndGameData
                {
                    GameController = GameController,
                    GameEndReason = endReason
                }
            );
        }
    }
}