using Gameplay.SpawnSystem.Abstracts;
using UnityEngine;
using Utils.GameTools;

namespace Gameplay.SpawnSystem.Components
{
    public class Area3DSpawnPoint : SpawnPoint
    {
        [Header("3D Area")]
        [SerializeField] private RandomPosition3D randomPosition3D;
        
        public override Vector3 GetSpawnPosition()
        {
            return randomPosition3D.SpawnPosition();
        }
    }
}