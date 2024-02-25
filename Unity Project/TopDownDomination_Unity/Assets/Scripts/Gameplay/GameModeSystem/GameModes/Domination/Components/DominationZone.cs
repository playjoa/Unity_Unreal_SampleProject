using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameModeSystem.GameModes.Domination.Data;
using Gameplay.SpawnSystem.Abstracts;
using Gameplay.SpawnSystem.Controller;
using UnityEngine;
using Utils.Extensions;

namespace Gameplay.GameModeSystem.GameModes.Domination.Components
{
    public class DominationZone : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private DominationZoneData zoneData;
        
        [Header("Components")]
        [SerializeField] private DominationCollider dominationCollider;
        [SerializeField] private SpawnPoint[] dominationSpawnPoints;

        public event Action<DominationZone> OnZoneCaptured;
        public event Action<DominationZone> OnZoneCaptureInProgress;
        
        public EntityType EntityOwnerType { get; private set; }
        public float CaptureProgress { get; private set; }
        public string DominationZoneName => zoneData.DominationZoneName;

        private readonly HashSet<IGameEntity> _currentZoneGuardians = new();
        private readonly HashSet<IGameEntity> _currentEntitiesInBase = new();
        private readonly WaitForSeconds _dominationTickWait = new(ZONE_TICK_RATE);

        private SpawnController SpawnController => GameController.ME.SpawnController;

        private const float ZONE_TICK_RATE = 0.5f;
        
        private float _captureTimer = 0f;
        private bool _captureInProgress = false;
        private bool _gameActive = true;
        
        public void Initiate()
        {
            InitiateGuardians(zoneData);
            CaptureProgress = 1;
            EntityOwnerType = EntityType.Enemy;
            
            dominationCollider.OnEntityEnterDominationArea += OnEntityEnterHandler;
            dominationCollider.OnEntityExitDominationArea += OnEntityExitHandler;
            
            StartCoroutine(DominationTickCoroutine());
        }

        private void OnDestroy()
        {
            _gameActive = false;
            dominationCollider.OnEntityEnterDominationArea -= OnEntityEnterHandler;
            dominationCollider.OnEntityExitDominationArea -= OnEntityExitHandler;
        }

        private void InitiateGuardians(DominationZoneData targetData)
        {
            if (targetData == null) return;
            if (targetData.GuardiansData == null || !targetData.GuardiansData.Any()) return;
            
            for (var i = 0; i < targetData.GuardiansAmount; i++)
            {
                var guardianData = targetData.GuardiansData.RandomElement();
                var spawnedEntity = SpawnController.SpawnEntity(guardianData, GetGuardianSpawnPosition(), Quaternion.identity);

                spawnedEntity.EntityHealth.OnDied += OnGuardianDiedHandler;
                _currentZoneGuardians.Add(spawnedEntity);   
            }
        }

        private Vector3 GetGuardianSpawnPosition()
        {
            if (!dominationSpawnPoints.Any())
                return transform.position;

            var randomSpawnPoint = dominationSpawnPoints.RandomElement();
            return randomSpawnPoint.GetSpawnPosition();
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
            if (EntityOwnerType == EntityType.Player) return;
            if (_currentZoneGuardians.Any()) return;
            
            if (_currentEntitiesInBase.Count == 1 && _currentEntitiesInBase.Any(e=> e.EntityType == EntityType.Player))
            {
                if (!_captureInProgress)
                {
                    _captureInProgress = true;
                    _captureTimer = 0f;
                }

                _captureTimer += ZONE_TICK_RATE;
                CaptureProgress = _captureTimer / zoneData.CaptureTime;
                OnZoneCaptureInProgress?.Invoke(this);
                
                if (CaptureProgress >= 1f)
                {
                    CaptureZone(EntityType.Player);
                }
            }
            else
            {
                _captureInProgress = false;
                _captureTimer = 0f;
                CaptureProgress = 0f;
                OnZoneCaptureInProgress?.Invoke(this);
            }
        }
        
        private void CaptureZone(EntityType newOwnerEntity)
        {
            EntityOwnerType = newOwnerEntity;
            _captureInProgress = false;
            _captureTimer = 0f;
            OnZoneCaptured?.Invoke(this);
        }

        private void OnGuardianDiedHandler(HealthChangeData healthChangeData)
        {
            healthChangeData.Victim.EntityHealth.OnDied -= OnGuardianDiedHandler;
            _currentZoneGuardians.Remove(healthChangeData.Victim);
        }
    }
}