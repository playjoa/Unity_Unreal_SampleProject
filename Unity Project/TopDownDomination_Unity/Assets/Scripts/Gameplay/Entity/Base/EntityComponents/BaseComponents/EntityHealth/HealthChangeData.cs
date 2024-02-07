using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.Components
{
    public struct HealthChangeData
    {
        public IGameEntity Victim;
        public IGameEntity Inflicter;
        public int DealtAmount;
    }
}