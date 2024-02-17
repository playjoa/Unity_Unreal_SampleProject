using UnityEngine;

namespace Gameplay.SpawnSystem.Abstracts
{
    public abstract class SpawnPoint : MonoBehaviour
    {
        public abstract Vector3 GetSpawnPosition();
    }
}