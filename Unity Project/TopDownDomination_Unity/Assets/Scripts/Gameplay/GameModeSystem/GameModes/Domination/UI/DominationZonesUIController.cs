using System.Collections.Generic;
using Gameplay.GameModeSystem.GameModes.Domination.Components;
using Gameplay.GameModeSystem.GameModes.Domination.Controller;
using Gameplay.GameModeSystem.UI;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    public class DominationZonesUIController : GameModeUIComponent<DominationGameMode>
    {
        [Header("Prefab")]
        [SerializeField] private DominationViewUI dominationViewPrefab;

        [Header("Rect Transform")] 
        [SerializeField] private RectTransform dominationViewsRect;

        private List<DominationViewUI> _dominationViews = new();
        
        protected override void OnInitiate(DominationGameMode currentGameMode)
        {
            foreach (var targetDominationZone in currentGameMode.AllDominationZones)
            {
                CreateDominationView(targetDominationZone);
            }
        }

        private void CreateDominationView(DominationZone dominationZone)
        {
            var newDominationView = Instantiate(dominationViewPrefab, dominationViewsRect);
            newDominationView.Initiate(dominationZone);
            _dominationViews.Add(newDominationView);
        }
    }
}