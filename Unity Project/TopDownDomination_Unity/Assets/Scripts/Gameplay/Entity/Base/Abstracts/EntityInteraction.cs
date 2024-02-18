using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.Abstracts
{
    public abstract class EntityInteraction : IEntityInteraction
    {
        public IGameEntity Owner { get; }
        public IGameEntity Target { get; }

        private bool _interactionComplete;
        
        protected EntityInteraction(IGameEntity owner, IGameEntity target)
        {
            Owner = owner;
            Target = target;
        }

        public void ExecuteInteraction()
        {
            if (_interactionComplete) return;

            InteractionBehaviour();
            
            _interactionComplete = true;
            Target.EntityInteractions.RegisterInteraction(this);
        }

        protected abstract void InteractionBehaviour();
    }
}