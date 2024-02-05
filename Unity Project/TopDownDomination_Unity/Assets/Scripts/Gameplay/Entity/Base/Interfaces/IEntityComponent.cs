namespace Gameplay.Entity.Base.Interfaces
{
    public interface IEntityComponent
    {
        IGameEntity Owner { get; }

        void Initiate(IGameEntity owner);
        void ReviveComponent();
    }
}