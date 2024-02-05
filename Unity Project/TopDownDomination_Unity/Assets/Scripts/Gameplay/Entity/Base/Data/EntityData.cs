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
        
        [Header("Components Data")]
        [SerializeField] private float entityBaseHealth;
        
        public string EntityName => entityName;
        public Sprite EntityIcon => entityIcon;

        public EntityType EntityType => entityType;
        
        public float EntityBaseHealth => entityBaseHealth;
    }
}