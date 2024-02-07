using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Components
{
    public class DominationZone : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private DominationCollider dominationCollider;

        public Action<DominationZone> OnZoneCaptured;
        public Action<DominationZone> OnZoneCaptureInProgress;
        
        public EntityType EntityOwnerType { get; private set; }
        public float CaptureProgress { get; private set; }
        public string DominationZoneName { get; private set; } = "A";

        private readonly List<IGameEntity> _currentZoneGuardians = new();
        private readonly HashSet<IGameEntity> _currentEntitiesInBase = new();
        private WaitForSeconds _dominationTickWait = new(0.5f);
        
        private bool _gameActive = true;
        
        public void Initiate()
        {
            dominationCollider.OnEntityEnterDominationArea += OnEntityEnterHandler;
            dominationCollider.OnEntityExitDominationArea += OnEntityExitHandler;

            StartCoroutine(DominationTickCoroutine());
        }

        private void OnDestroy()
        {
            dominationCollider.OnEntityEnterDominationArea += OnEntityEnterHandler;
            dominationCollider.OnEntityExitDominationArea += OnEntityExitHandler;
        }
        
        private void OnEntityEnterHandler(IGameEntity gameEntity)
        {
            _currentEntitiesInBase.Add(gameEntity);
        }

        private void OnEntityExitHandler(IGameEntity gameEntity)
        {
            _currentEntitiesInBase.Remove(gameEntity);
        }

        private IEnumerator DominationTickCoroutine()
        {
            while (_gameActive)
            {
                yield return _dominationTickWait;
                ProcessDominationZoneTick();
            }
        }

        private void ProcessDominationZoneTick()
        {
            if (_currentZoneGuardians.Any()) return;
        }
    }
}