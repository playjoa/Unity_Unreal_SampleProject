using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.GameModes.Domination.Data;
using Gameplay.SpawnSystem.Controller;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Components
{
    public class DominationZone : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private DominationZoneData zoneData;
        
        [Header("Components")]
        [SerializeField] private DominationCollider dominationCollider;

        public Action<DominationZone> OnZoneCaptured;
        public Action<DominationZone> OnZoneCaptureInProgress;
        
        public EntityType EntityOwnerType { get; private set; }
        public float CaptureProgress { get; private set; }
        public string DominationZoneName => zoneData.DominationZoneName;

        private readonly List<IGameEntity> _currentZoneGuardians = new();
        private readonly HashSet<IGameEntity> _currentEntitiesInBase = new();
        private readonly WaitForSeconds _dominationTickWait = new(ZONE_TICK_RATE);

        private SpawnController SpawnController => GameController.ME.SpawnController;

        private const float ZONE_TICK_RATE = 0.5f;
        
        private float _captureTimer = 0f;
        private bool _captureInProgress = false;
        private bool _gameActive = true;
        
        public void Initiate()
        {
            InitiateGuardians(zoneData.GuardiansData);
            
            dominationCollider.OnEntityEnterDominationArea += OnEntityEnterHandler;
            dominationCollider.OnEntityExitDominationArea += OnEntityExitHandler;

            StartCoroutine(DominationTickCoroutine());
        }

        private void OnDestroy()
        {
            dominationCollider.OnEntityEnterDominationArea += OnEntityEnterHandler;
            dominationCollider.OnEntityExitDominationArea += OnEntityExitHandler;
        }

        private void InitiateGuardians(List<EntityData> guardiansData)
        {
            foreach (var guardianData in guardiansData)
            {
                var spawnedEntity = SpawnController.SpawnEntity(guardianData, transform.position, Quaternion.identity);

                spawnedEntity.EntityHealth.OnDied += OnGuardianDiedHandler;
                _currentZoneGuardians.Add(spawnedEntity);
            }
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
            
            if (_currentEntitiesInBase.Count == 1 && _currentEntitiesInBase.Any(e=> e.EntityType == EntityType.Player))
            {
                if (!_captureInProgress)
                {
                    OnZoneCaptureInProgress?.Invoke(this);
                    _captureInProgress = true;
                    _captureTimer = 0f;
                }

                _captureTimer += ZONE_TICK_RATE;
                CaptureProgress = _captureTimer / zoneData.CaptureTime;

                if (CaptureProgress >= 1f)
                {
                    CaptureZone();
                }
            }
            else
            {
                _captureInProgress = false;
                _captureTimer = 0f;
            }
        }
        
        private void CaptureZone()
        {
            OnZoneCaptured?.Invoke(this);
            // Reset capture progress and any other necessary variables
            _captureInProgress = false;
            _captureTimer = 0f;
        }

        private void OnGuardianDiedHandler(HealthChangeData healthChangeData)
        {
            healthChangeData.Victim.EntityHealth.OnDied -= OnGuardianDiedHandler;
            _currentZoneGuardians.Remove(healthChangeData.Victim);
        }
    }
}