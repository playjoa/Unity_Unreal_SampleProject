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
        public List<DominationZone> AllDominationZones { get; private set; } = new();

        public event Action<DominationGameMode> OnScoreUpdated;

        public int CurrentDominationPoints { get; private set; }
        public int TargetDominationPoints { get; private set; }
        public float DominationPointsProgress => CurrentDominationPoints / (float)TargetDominationPoints;

        public DominationGameMode(DominationGameModeData gameModeData, GameController gameController) : base(gameModeData, gameController)
        {
            TargetDominationPoints = gameModeData.ScoreTarget;
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

            CurrentDominationPoints = Mathf.Clamp(CurrentDominationPoints + pointsToAdd, 0, GameModeConfigData.ScoreTarget);
            OnScoreUpdated?.Invoke(this);
        }

        private bool ProcessDominationPoints()
        {
            if (CurrentDominationPoints >= GameModeConfigData.ScoreTarget)
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