using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Data
{
    [CreateAssetMenu(menuName = "GameSystems/GameModes/UI/DominationViewData", fileName = "New DominationViewData")]
    public class DominationViewData : ScriptableObject
    {
        [Header("Colors")]
        public Color DominatedBaseColor = Color.red;
        public Color OwnedByPlayerColor = Color.blue;
    }
}