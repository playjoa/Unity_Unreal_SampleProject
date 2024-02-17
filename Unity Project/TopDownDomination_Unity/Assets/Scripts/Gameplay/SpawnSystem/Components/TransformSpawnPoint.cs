using Gameplay.SpawnSystem.Abstracts;
using UnityEngine;

namespace Gameplay.SpawnSystem.Components
{
    public class TransformSpawnPoint : SpawnPoint
    {
        [Header("Transform")]
        [SerializeField] private Transform targetSpawnPointTransform;
        
        public override Vector3 GetSpawnPosition()
        {
            return targetSpawnPointTransform.position;
        }
    }
}