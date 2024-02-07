using System.Collections;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using UnityEngine;

namespace Gameplay.SpawnSystem.Controller
{
    public class SpawnController : MonoBehaviour, IGameplaySystem
    {
        public IGameEntity PlayerEntity { get; private set; }
        // public MapSpawnPointsController MapSpawnPoints { get; private set; }
        
        public IEnumerator Initiate(GameController gameController)
        {
            Debug.Log($"----Initializing SpawnController----");
            // yield return new WaitUntil(() => MapSceneryController.ME != null);
            
            yield return true;
            
            /*
             MapSpawnPoints = MapSceneryController.ME.SpawnPointsController;
            
            if (!MapSpawnPoints.InitiateSpawns(out var playerUnit))
            {
                Debug.LogError("Error Spawning Player");
                yield break;
            }
            */

            // PlayerEntity = playerUnit;
            
            Debug.Log($"----Done Initializing SpawnController----");
        }

        public void OnCleanUp()
        {
            
        }
    }
}