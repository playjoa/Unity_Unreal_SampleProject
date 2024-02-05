using Gameplay.Entity.Base.Interfaces;

namespace Gameplay.Entity.Base.Components
{
    public struct HealthUpdatePackageData
    {
        public IGameEntity Inflicter;
        public int Delta;
    }
}