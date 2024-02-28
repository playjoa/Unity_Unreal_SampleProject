using System;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractableSystem.Data;

namespace Gameplay.GameInteractableSystem.Interfaces
{
    public interface IGameInteractable
    {
        event Action OnStartInteraction;
        event Action OnEndInteraction;
        
        bool Interacting { get; }
        bool CanInteract { get; }
        
        InteractData InteractData { get; }
        IGameEntity CurrentInteractingGameEntity { get; }
        
        bool TryStartInteraction(IGameEntity gameEntity);
        bool InteractionUpdateTick();
        void EndInteraction();
    }
}