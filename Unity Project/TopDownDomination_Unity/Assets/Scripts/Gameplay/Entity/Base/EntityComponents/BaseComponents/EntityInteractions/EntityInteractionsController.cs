using System;
using System.Collections.Generic;
using Gameplay.Entity.Base.Abstracts;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityInteractions
{
    public class EntityInteractionsController : BaseEntityComponent
    {
        public event Action<EntityInteraction> OnInteractionRegistered;
        
        public List<EntityInteraction> EntityInteractions { get; private set; }

        protected override void OnRevive()
        {
            EntityInteractions.Clear();
        }

        public void RegisterInteraction(EntityInteraction entityInteraction)
        {
            EntityInteractions.Add(entityInteraction);
            OnInteractionRegistered?.Invoke(entityInteraction);
        }
    }
}