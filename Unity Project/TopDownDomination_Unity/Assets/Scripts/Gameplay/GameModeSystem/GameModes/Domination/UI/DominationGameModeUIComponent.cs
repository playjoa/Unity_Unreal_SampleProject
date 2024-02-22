using Gameplay.GameModeSystem.GameModes.Domination.Controller;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    public abstract class DominationGameModeUIComponent : MonoBehaviour
    {
        protected DominationGameMode DominationGameMode { get; private set; }

        public void Initiate(DominationGameMode dominationGameMode)
        {
            DominationGameMode = dominationGameMode;
            OnInitiate(DominationGameMode);
        }

        protected virtual void OnInitiate(DominationGameMode dominationGameMode)
        {
            
        }
    }
}