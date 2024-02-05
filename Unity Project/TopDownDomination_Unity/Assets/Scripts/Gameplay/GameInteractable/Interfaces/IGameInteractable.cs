using Gameplay.GameInteractable.Data;

namespace Gameplay.GameInteractable.Interfaces
{
    public interface IGameInteractable
    {
        InteractData InteractData { get; }
        void Interact();
    }
}