using System.Collections.Generic;
using Gameplay.Entity.Base.Components;
using Gameplay.Entity.Base.Data;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityBrain.Base;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityGraphics;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions;
using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityMovement;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Abstracts
{
    public abstract class BaseEntity : MonoBehaviour, IGameEntity
    {
        [Header("Base Components")]
        [SerializeField] private EntityBrainController entityBrainController;
        [SerializeField] private EntityHealth entityHealth;
        [SerializeField] private EntityGraphicsController entityGraphics;
        [SerializeField] private EntityMovementBase entityMovement;
        [SerializeField] private EntityInteractionsController entityInteractions;

        public bool IsActive => !entityHealth.IsDead && gameObject.activeSelf;
        public EntityType EntityType => EntityData != null ? EntityData.EntityType : EntityType.Unknown;
        
        public EntityData EntityData { get; private set; }
        public EntityBrainController EntityBrain => entityBrainController;
        public EntityHealth EntityHealth => entityHealth;
        public EntityGraphicsController EntityGraphics => entityGraphics;
        public EntityMovementBase EntityMovement => entityMovement;
        public EntityInteractionsController EntityInteractions => entityInteractions;
        
        public Transform EntityTransform => transform;

        private readonly List<IEntityComponent> _baseComponents = new();
        private readonly List<IEntityExtraComponent> _extraComponents = new();
        
        public void Initiate(EntityData entityData)
        {
            EntityData = entityData;
            
            InitiateBaseComponents();
            InitiateExtraComponents();

            entityHealth.OnDied += OnEntityDiedHandler;
        }

        public void Revive()
        {
            foreach (var baseComponent in _baseComponents)
            {
                baseComponent.ReviveComponent();
            }
            
            foreach (var extraComponent in _extraComponents)
            {
                extraComponent.ReviveComponent();
            }

            OnRevived();
        }
        
        private void OnDestroy()
        {
            foreach (var baseComponent in _baseComponents)
            {
                baseComponent.Clean();
            }
            
            foreach (var extraComponent in _extraComponents)
            {
                extraComponent.Clean();
            }
        }
        
        private void InitiateBaseComponents()
        {
            _baseComponents.Add(entityBrainController);
            _baseComponents.Add(entityHealth);
            _baseComponents.Add(entityGraphics);
            _baseComponents.Add(entityMovement);
            _baseComponents.Add(entityInteractions);

            foreach (var baseComponent in _baseComponents)
            {
                baseComponent.Initiate(this);
            }
        }
        
        private void InitiateExtraComponents()
        {
            var extraComponentsInEntity = EntityTransform.GetComponentsInChildren<IEntityExtraComponent>(true);

            foreach (var entityExtraComponent in extraComponentsInEntity)
            {
                _extraComponents.Add(entityExtraComponent);
                entityExtraComponent.Initiate(this);
            }
        }

        public bool TryGetExtraComponent<TExtraComponent>(out TExtraComponent targetComponent) where TExtraComponent : IEntityExtraComponent
        {
            targetComponent = default;
            foreach (var extraComponent in _extraComponents)
            {
                if (extraComponent is not TExtraComponent targetExtraComponent) continue;
                
                targetComponent = targetExtraComponent;
                return true;
            }

            return false;
        }

        protected virtual void OnDied()
        {
        }
        
        protected virtual void OnRevived()
        {
        }
        
        
        private void OnEntityDiedHandler(HealthChangeData healthChangeData)
        {
            OnDied();
        }
    }
}