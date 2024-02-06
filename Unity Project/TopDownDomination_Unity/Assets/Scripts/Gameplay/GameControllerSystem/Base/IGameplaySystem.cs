using System.Collections;
using Gameplay.GameControllerSystem.Controller;

namespace Gameplay.GameControllerSystem.Base
{
    public interface IGameplaySystem
    {
        IEnumerator Initiate(GameController gameplaySystem);
        void OnEnd();
    }
}