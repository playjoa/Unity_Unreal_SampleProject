using System.Collections;
using System.Collections.Generic;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.SpawnSystem.Abstracts;
using GameWideSystems.GameDataSystem.Controller;
using UnityEngine;
using Utils.Extensions;
using Utils.SerializableUtils;

namespace Gameplay.SpawnSystem.Controller
{
    public class SpawnController : MonoBehaviour, IGameplaySystem
    {
        [Header("Prefab Models")] 
        [SerializeField] private SerializableDictio<EntityType, BaseEntity> entityPrefabs;
        
        public IGameEntity PlayerEntity { get; private set; }

        private GameDataController GameData => GameDataController.ME;

        public IEnumerator Initiate(GameController gameController)
        {
            Debug.Log($"----Initializing SpawnController----");
            entityPrefabs.Initiate();
            yield return true;
            SpawnPlayer(gameController.MapContentController.PlayerSpawnPoints);
            
            Debug.Log($"----Done Initializing SpawnController----");
        }
        
        public void OnCleanUp()
        {
            
        }

        private void SpawnPlayer(List<SpawnPoint> spawnPoints)
        {
            var randomPlayerSpawn = spawnPoints.RandomElement();
            var spawnPos = randomPlayerSpawn.GetSpawnPosition();

            var playerUnit = SpawnEntity(GameData.CurrentPlayerEntityData, spawnPos, Quaternion.identity);
            PlayerEntity = playerUnit;
        }

        public IGameEntity SpawnEntity(EntityData entityData, Vector3 position, Quaternion rotation)
        {
            if (!TryGetPrefabModel(entityData, out var entityPrefabModel))
            {
                return default;                
            }

            var spawnedEntity = Instantiate(entityPrefabModel, position, rotation);
            spawnedEntity.Initiate(entityData);

            return spawnedEntity;
        }

        private bool TryGetPrefabModel(EntityData entityData, out BaseEntity baseEntity)
        {
            baseEntity = default;
            if (entityPrefabs.TryGetValue(entityData.EntityType, out baseEntity))
            {
                return true;
            }

            return false;
        }
    }
}