namespace Gameplay.Entity.Base.Interfaces
{
    public interface IEntityInteraction
    {
        IGameEntity Owner { get; }
        IGameEntity Target { get; }
        void ExecuteInteraction();
    }
}