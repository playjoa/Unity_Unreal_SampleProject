using Gameplay.Entity.Base.Abstracts;
using UnityEngine;

namespace Gameplay.Entity.Enemy
{
    public class EnemyEntity : BaseEntity
    {
        [Header("Collider Config.")]
        [SerializeField] private Collider enemyCollider;

        protected override void OnRevived()
        {
            enemyCollider.enabled = true;
        }

        protected override void OnDied()
        {
            enemyCollider.enabled = false;
        }
    }
}