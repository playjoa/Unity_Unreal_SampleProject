using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.UI.Interfaces
{
    public interface IPlayerUI
    {
        void Initiate(IGameEntity playerEntity);
        void ToggleUI(bool valueToSet);
    }
}