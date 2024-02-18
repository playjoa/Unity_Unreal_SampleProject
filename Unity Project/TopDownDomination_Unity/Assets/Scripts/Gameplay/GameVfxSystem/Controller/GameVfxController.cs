using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Base;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameVfxSystem.Components;
using Gameplay.GameVfxSystem.Data;
using Unity.Mathematics;
using UnityEngine;

namespace Gameplay.GameVfxSystem.Controller
{
    public class GameVfxController : MonoBehaviour, IGameplaySystem
    {
        private readonly Dictionary<string, Queue<GameVfx>> _gameVfxPool = new();
        
        public IEnumerator Initiate(GameController gameplaySystem)
        {
            yield return true;
        }

        public void OnCleanUp()
        {
            
        }

        public GameVfx SpawnVfx(VfxData data, IGameEntity requester, Vector3 position, float overrideDuration = -1)
        {
            if (data == null) return default;

            TryToCreateVfxQueue(data.Id);

            var vfxQueue = GetVfxQueue(data.Id);

            if (vfxQueue.Any())
            {
                var vfx = vfxQueue.Dequeue();

                vfx.transform.position = position + data.StartPositionOffSet;
                InitiateVfx(vfx);
                return vfx;
            }

            var newVfx = Instantiate(data.VfxPrefab, position + data.StartPositionOffSet, quaternion.identity);
            InitiateVfx(newVfx);
            return newVfx;

            void InitiateVfx(GameVfx vfx)
            {
                vfx.Initiate(requester, data, overrideDuration);
            }
        }

        public void ReturnVfx(GameVfx gameVfx)
        {
            TryToCreateVfxQueue(gameVfx.VfxId);

            var vfxQueue = GetVfxQueue(gameVfx.VfxId);

            gameVfx.DisableVfx();
            vfxQueue.Enqueue(gameVfx);
        }

        private Queue<GameVfx> GetVfxQueue(string vfxId)
        {
            if (_gameVfxPool.TryGetValue(vfxId, out var vfxQueue))
                return vfxQueue;

            return new Queue<GameVfx>();
        }

        private void TryToCreateVfxQueue(string vfxId)
        {
            if (_gameVfxPool.ContainsKey(vfxId)) return;

            _gameVfxPool.Add(vfxId, new Queue<GameVfx>());
        }
    }
}