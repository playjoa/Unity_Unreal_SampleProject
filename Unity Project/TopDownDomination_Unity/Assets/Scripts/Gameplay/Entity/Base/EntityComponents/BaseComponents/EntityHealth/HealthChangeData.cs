using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.Components
{
    public struct HealthChangeData
    {
        public IGameEntity Inflicter;
        public int DealtAmount;
    }
}