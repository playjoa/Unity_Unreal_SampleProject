using System;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractable.Data;

namespace Gameplay.GameInteractable.Interfaces
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