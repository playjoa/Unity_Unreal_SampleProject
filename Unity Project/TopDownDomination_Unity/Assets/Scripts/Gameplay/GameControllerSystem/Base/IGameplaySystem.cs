using System.Collections;
using Gameplay.GameControllerSystem.Controller;

namespace Gameplay.GameControllerSystem.Base
{
    public interface IGameplaySystem
    {
        public IEnumerator Initiate(GameController gameController);
    }
}