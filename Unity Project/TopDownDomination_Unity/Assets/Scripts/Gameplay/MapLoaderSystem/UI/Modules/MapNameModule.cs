using Gameplay.MapLoaderSystem.Abstracts;
using Gameplay.MapLoaderSystem.Data;
using TMPro;
using UnityEngine;

namespace Gameplay.MapLoaderSystem.UI.Modules
{
    public class MapNameModule : MapLoadingScreenModule
    {
        [Header("Name Text")]
        [SerializeField] private TextMeshProUGUI mapNameTMP;
        
        public override void OnInitiated(MapData mapData)
        {
            mapNameTMP.text = mapData.MapName;
        }
    }
}