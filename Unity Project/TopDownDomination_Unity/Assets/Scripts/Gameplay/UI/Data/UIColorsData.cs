using UnityEngine;

namespace Gameplay.UI.Data
{
    [CreateAssetMenu(menuName = "GameSystems/UI/UIColorsData", fileName = "New UIColorsData")]
    public class UIColorsData : ScriptableObject
    {
        [Header("Interaction Colors")]
        public Color selectedUIColor;
        public Color notSelectedUIColor;
    }
}