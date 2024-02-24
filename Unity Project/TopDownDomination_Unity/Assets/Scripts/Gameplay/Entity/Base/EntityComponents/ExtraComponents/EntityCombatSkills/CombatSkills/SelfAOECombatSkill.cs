using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Controller;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Data;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.Entity.Base.Utils;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameVfxSystem.Controller;
using Gameplay.GameVfxSystem.Data;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.CombatSkills
{
    public class SelfAOECombatSkill : CombatSkill<AOECombatSkillData>
    {
        private static GameVfxController VfxController => GameController.ME.GameVfxController;
        
        private readonly Collider[] _results = new Collider[10];
        
        public SelfAOECombatSkill(AOECombatSkillData data, EntityCombatSkillsController skillsController) : base(data, skillsController)
        {
        }

        protected override void SkillBehaviour(CombatSkillRequestPackage requestPackage)
        {
            var size = Physics.OverlapSphereNonAlloc
            (
                requestPackage.CasterPosition,
                CombatSkillData.Range,
                _results,
                LayerUtils.EntityLayerIndex
            );
            
            SpawnVfx(CombatSkillData.VfxToPlay, requestPackage.CasterPosition);
            
            for (var i = 0; i < size; i++)
            {
                var collider = _results[i];
                if (collider == null) continue;
                
                if (collider.TryGetComponent<IGameEntity>(out var entity)) continue;
                if (CombatSkillData.TargetsHitEntities.Contains(entity.EntityType)) continue;
                if (!CombatSkillData.TargetsAvoidEntities.Contains(entity.EntityType)) continue;
                
                AffectEntity(entity);
            }
        }

        private void AffectEntity(IGameEntity targetEntity)
        {
            var entityInteraction = CombatSkillData.InteractionData.GenerateInteraction(Owner, targetEntity);
            entityInteraction.ExecuteInteraction();
        }
        
        private void SpawnVfx(VfxData targetVfx, Vector3 targetPosition)
        {
            VfxController.SpawnVfx(targetVfx, Owner, targetPosition);
        }
    }
}