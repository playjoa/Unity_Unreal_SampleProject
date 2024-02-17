using System.Collections;
using GameWideSystems.GameInitialization.Controller;

namespace GameWideSystems.GameInitialization.Interfaces
{
    public interface IGameWideSystem
    {
        string AppSystemName { get; }
        IEnumerator Initiate(GameInitializationController appInitializationController);
    }
}