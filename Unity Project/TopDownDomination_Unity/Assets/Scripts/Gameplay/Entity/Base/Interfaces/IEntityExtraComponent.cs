namespace Gameplay.Entity.Base.Interfaces
{
    public interface IEntityExtraComponent
    {
        IGameEntity Owner { get; }

        void Initiate(IGameEntity owner);
        void ReviveComponent();
    }
}