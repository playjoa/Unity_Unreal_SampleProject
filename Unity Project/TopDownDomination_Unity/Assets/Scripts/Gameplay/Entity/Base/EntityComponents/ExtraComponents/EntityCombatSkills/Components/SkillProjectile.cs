using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Utils;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.Entity.Base.Utils;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Components
{
    public class SkillProjectile : MonoBehaviour
    {
        [Header("Physics Components")]
        [SerializeField] private Rigidbody projectileRigidbody;
        [SerializeField] private Collider projectileCollider;

        [Header("Graphics")] 
        [SerializeField] private GameObject projectileGraphics;
        
        [Header("VFX")] 
        [SerializeField] private float vfxDuration = 0;
        [SerializeField] private GameObject hitVfxHolder;
        
        public event Action OnExploded;
        
        public Vector3 StartPosition { get; private set; }
        public IGameEntity Owner { get; private set; }

        private readonly List<IGameEntity> _targetsToAvoid = new();

        private Coroutine _lifeTimeCoroutine;

        public ProjectileCombatSkillData ProjectileSkillData { get; private set; }

        private void Awake()
        {
            hitVfxHolder.SetActive(false);
        }

        private void OnDisable()
        {
            projectileCollider.enabled = true;
            projectileGraphics.SetActive(true);
            hitVfxHolder.SetActive(false);
        }
        
        public void Initiate(IGameEntity owner, ProjectileCombatSkillData projectileCombatSkillData)
        {
            Owner = owner;
            ProjectileSkillData = projectileCombatSkillData;
            
            _targetsToAvoid.Clear();
            _targetsToAvoid.Add(owner);
            
            LaunchProjectile();
        }

        public void AddTargetToIgnore(IGameEntity gameEntity)
        {
            _targetsToAvoid.Add(gameEntity);
        }
        
        private void LaunchProjectile()
        {
            StartPosition = transform.position;
            _lifeTimeCoroutine = StartCoroutine(CombatSkillsUtils.ProjectileLifetime(this, Explode));
            AddForceToProjectile();
        }
        
        private void AddForceToProjectile()
        {
            projectileRigidbody.AddForce(GetStartingForce(), ForceMode.Impulse);
        }
        
        private Vector3 GetStartingForce()
        {
            var forward = transform.forward;
            return forward * ProjectileSkillData.ProjectileVelocity;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!ValidateTargetLayer(other.gameObject)) return;
            
            if (!other.TryGetComponent<IGameEntity>(out var targetGameEntity))
            {
                Explode();
                return;
            }

            if (_targetsToAvoid.Any(t => t == targetGameEntity)) return;
            
            if (ProjectileSkillData.TargetsHitEntities.All(t => t != targetGameEntity.EntityType))
            {
                Explode();
                return;
            }
            
            if (ProjectileSkillData.TargetsAvoidEntities.Any(t => t == targetGameEntity.EntityType))
            {
                Explode();
                return;
            }
            
            AffectEntity(targetGameEntity);
            Explode();
        }
        
        private bool ValidateTargetLayer(GameObject targetGameObject)
        {
            if (targetGameObject.layer == LayerUtils.GroundLayerIndex) return true;
            if (targetGameObject.layer == LayerUtils.EntityLayerIndex) return true;

            return false;
        }

        private void AffectEntity(IGameEntity targetEntity)
        {
            var entityInteraction = ProjectileSkillData.InteractionData.GenerateInteraction(Owner, targetEntity);
            entityInteraction.ExecuteInteraction();
        }

        private void Explode()
        {
            projectileRigidbody.velocity = Vector3.zero;
            
            if (_lifeTimeCoroutine != null)
                StopCoroutine(_lifeTimeCoroutine);
            
            StartCoroutine(ExplodeVfx(DisableProjectile));
            OnExploded?.Invoke();
        }

        protected virtual IEnumerator ExplodeVfx(Action onDisable)
        {
            projectileCollider.enabled = false;
            projectileGraphics.SetActive(false);
            hitVfxHolder.SetActive(true);
            yield return new WaitForSeconds(vfxDuration);
            onDisable.Invoke();
        }

        protected virtual void DisableProjectile()
        {
            Destroy(this);
        }
    }
}