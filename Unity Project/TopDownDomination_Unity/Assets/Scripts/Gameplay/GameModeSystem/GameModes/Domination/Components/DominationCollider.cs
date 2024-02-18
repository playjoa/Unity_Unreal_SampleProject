﻿using System;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Components
{
    public class DominationCollider : MonoBehaviour
    {
        public event Action<IGameEntity> OnEntityEnterDominationArea;
        public event Action<IGameEntity> OnEntityExitDominationArea;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IGameEntity>(out var gameEntity)) return;
            if (!gameEntity.IsActive) return;
            
            OnEntityEnterDominationArea?.Invoke(gameEntity);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IGameEntity>(out var gameEntity)) return;
            if (!gameEntity.IsActive) return;

            OnEntityExitDominationArea?.Invoke(gameEntity);
        }
    }
}