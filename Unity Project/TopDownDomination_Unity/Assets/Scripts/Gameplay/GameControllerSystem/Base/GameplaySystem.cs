using System.Collections;
using Gameplay.GameControllerSystem.Controller;
using UnityEngine;

namespace Gameplay.GameControllerSystem.Base
{
    public abstract class GameplaySystem : MonoBehaviour, IGameplaySystem
    {
        public IEnumerator Initiate(GameController missing_name)
        {
            throw new System.NotImplementedException();
        }
    }
}