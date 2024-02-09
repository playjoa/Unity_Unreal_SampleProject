using Gameplay.GameModeSystem.GameModes.Domination.Components;
using TMPro;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    public class DominationViewUI : MonoBehaviour
    {
        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI dominationNameTMP;
        
        public void Initiate(DominationZone dominationZone)
        {
            dominationNameTMP.text = dominationZone.DominationZoneName;
        }
    }
}