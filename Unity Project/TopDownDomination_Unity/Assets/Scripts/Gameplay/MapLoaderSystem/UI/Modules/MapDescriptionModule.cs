using Gameplay.MapLoaderSystem.Abstracts;
using Gameplay.MapLoaderSystem.Data;
using TMPro;
using UnityEngine;

namespace Gameplay.MapLoaderSystem.UI.Modules
{
    public class MapDescriptionModule : MapLoadingScreenModule
    {
        [Header("Description Text")]
        [SerializeField] private TextMeshProUGUI mapDescriptionTMP;
        
        public override void OnInitiated(MapData mapData)
        {
            mapDescriptionTMP.text = mapData.MapDescription;
        }
    }
}