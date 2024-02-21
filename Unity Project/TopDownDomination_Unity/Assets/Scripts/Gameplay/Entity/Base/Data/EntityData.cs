using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityMovement;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using UnityEngine;
using Utils.UniqueId.Components;

namespace Gameplay.Entity.Base.Data
{
    [CreateAssetMenu(fileName = "New EntityData", menuName = "Entity/EntityData", order = 1)]
    public class EntityData : ScriptableObjectWithId
    {
        [Header("Meta Data")]
        [SerializeField] private string entityName;
        [SerializeField] private Sprite entityIcon;
        
        [Header("Entity Stats")]
        [SerializeField] [Range(0, 10)] private int damageStatValue = 5;
        [SerializeField] [Range(0, 10)] private int speedStatValue = 5;
        
        [Header("Entity View")]
        [SerializeField] private GameObject entityGraphicView;
        [SerializeField] private Vector3 entityScale = Vector3.one;
        
        [Header("Entity Config.")]
        [SerializeField] private EntityType entityType;

        [Header("Base Components Config.")]
        [SerializeField] private HealthData healthData = new HealthData();
        [SerializeField] private MovementData movementData = new MovementData();

        [Header("Combat Skills Data")] 
        [SerializeField] private CombatSkillData primarySkillData;
        [SerializeField] private CombatSkillData secondarySkillData;
        
        public string EntityName => entityName;
        public Sprite EntityIcon => entityIcon;
        
        public int DamageStatValue => damageStatValue;
        public int SpeedStatValue => speedStatValue;
        
        public GameObject EntityGraphicView => entityGraphicView;
        public Vector3 EntityScale => entityScale;

        public EntityType EntityType => entityType;
        
        public HealthData HealthData => healthData;
        public MovementData MovementData => movementData;
        
        public CombatSkillData PrimarySkillData => primarySkillData;
        public CombatSkillData SecondarySkillData => secondarySkillData;
    }
}