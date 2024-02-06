using UnityEngine;
using Utils.UniqueId.Components;

namespace Gameplay.Entity.Base.Data
{
    [CreateAssetMenu(fileName = "EntityData", menuName = "Entity/New EntityData", order = 1)]
    public class EntityData : ScriptableObjectWithId
    {
        [Header("Meta Data")]
        [SerializeField] private string entityName;
        [SerializeField] private Sprite entityIcon;
        
        [Header("Entity Data")]
        [SerializeField] private EntityType entityType;
        
        [Header("Health Data")]
        [SerializeField] private bool isInvulnerable;
        [SerializeField] private int entityBaseHealth;
        
        public string EntityName => entityName;
        public Sprite EntityIcon => entityIcon;

        public EntityType EntityType => entityType;
        
        public bool IsInvulnerable => isInvulnerable;
        public int EntityBaseHealth => entityBaseHealth;
    }
}