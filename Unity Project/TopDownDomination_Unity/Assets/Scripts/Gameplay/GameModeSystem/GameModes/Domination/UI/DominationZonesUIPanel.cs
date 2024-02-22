using System.Collections.Generic;
using Gameplay.GameModeSystem.GameModes.Domination.Components;
using Gameplay.GameModeSystem.GameModes.Domination.Controller;
using Gameplay.GameModeSystem.UI;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    public class DominationZonesUIPanel : GameModeUIComponent<DominationGameMode>
    {
        [Header("Prefabs.")]
        [SerializeField] private DominationViewUI dominationViewPrefab;

        [Header("Target Rect")] 
        [SerializeField] private RectTransform dominationViewsRect;
        
        [Header("UI Components")]
        [SerializeField] private DominationGameModeUIComponent[] dominationUIComponents;
        
        private List<DominationViewUI> _dominationViews = new();
        
        protected override void OnInitiate(DominationGameMode currentGameMode)
        {
            foreach (var targetDominationZone in currentGameMode.AllDominationZones)
            {
                CreateDominationView(targetDominationZone);
            }

            foreach (var dominationUIComponent in dominationUIComponents)
            {
                dominationUIComponent.Initiate(currentGameMode);
            }
        }

        private void CreateDominationView(DominationZone dominationZone)
        {
            var newDominationView = Instantiate(dominationViewPrefab, dominationViewsRect);
            newDominationView.Initiate(dominationZone);
            _dominationViews.Add(newDominationView);
        }
        
        public void UpdateChildModules()
        {
            if (!Application.isEditor) return;

            dominationUIComponents = GetComponentsInChildren<DominationGameModeUIComponent>();
        }
    }
}