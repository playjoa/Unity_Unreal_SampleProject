using Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions.Base;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameControllerSystem.Controller;
using Gameplay.GameVfxSystem.Controller;

namespace Gameplay.Entity.Base.Abstracts
{
    public abstract class EntityInteraction<TInteractionData> : EntityInteraction where TInteractionData : EntityInteractionData
    {
        public TInteractionData InteractionData { get; private set; }

        protected static GameVfxController VfxController => GameController.ME.GameVfxController;
        
        private bool _interactionComplete;
        
        protected EntityInteraction(IGameEntity owner, IGameEntity target, TInteractionData interactionData) : base(owner, target)
        {
            InteractionData = interactionData;
        }
        
        public override void ExecuteInteraction()
        {
            if (_interactionComplete) return;

            InteractionBehaviour();
            PlayInteractionVfx();
            
            _interactionComplete = true;
            Target.EntityInteractions.RegisterInteraction(this);
        }

        private void PlayInteractionVfx()
        {
            if (InteractionData.vfxToPlayOnOwner != null)
            {
                VfxController.SpawnVfx(InteractionData.vfxToPlayOnOwner, Owner, Owner.EntityTransform.position);
            }
            
            if (InteractionData.vfxToPlayOnTarget != null)
            {
                VfxController.SpawnVfx(InteractionData.vfxToPlayOnTarget, Target, Target.EntityTransform.position);
            }
        }
    }
    
    public abstract class EntityInteraction : IEntityInteraction
    {
        public IGameEntity Owner { get; }
        public IGameEntity Target { get; }
        
        protected EntityInteraction(IGameEntity owner, IGameEntity target)
        {
            Owner = owner;
            Target = target;
        }

        public abstract void ExecuteInteraction();

        protected abstract void InteractionBehaviour();
    }
}