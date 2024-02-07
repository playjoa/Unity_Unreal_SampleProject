using Gameplay.GameControllerSystem.Controller;

namespace Gameplay.UI.Interfaces
{
    public interface IGameUI
    {
        void Initiate(GameController gameController);
        void ToggleUI(bool valueToSet);
    }
}