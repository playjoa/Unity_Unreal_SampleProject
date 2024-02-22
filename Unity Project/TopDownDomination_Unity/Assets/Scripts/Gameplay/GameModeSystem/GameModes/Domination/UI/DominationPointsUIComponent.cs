using Gameplay.GameModeSystem.GameModes.Domination.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    public class DominationPointsUIComponent : DominationGameModeUIComponent
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI targetPointsTMP;
        [SerializeField] private TextMeshProUGUI currentPointsTMP;

        [Header("Images")] 
        [SerializeField] private Image pointsProgressFillImage;
        
        protected override void OnInitiate(DominationGameMode dominationGameMode)
        {
            targetPointsTMP.text = $"Reach {dominationGameMode.TargetDominationPoints.ToNiceCurrency()} to win";
            UpdateCurrentPointsView(dominationGameMode);
            
            DominationGameMode.OnScoreUpdated += OnDominationPointsChangedHandler;
        }

        private void OnDestroy()
        {
            if (DominationGameMode == null) return;
            
            DominationGameMode.OnScoreUpdated -= OnDominationPointsChangedHandler;
        }

        private void OnDominationPointsChangedHandler(DominationGameMode dominationGameMode)
        {
            UpdateCurrentPointsView(dominationGameMode);
        }

        private void UpdateCurrentPointsView(DominationGameMode dominationGameMode)
        {
            currentPointsTMP.text = dominationGameMode.CurrentDominationPoints.ToNiceCurrency();
            pointsProgressFillImage.fillAmount = dominationGameMode.DominationPointsProgress;
        }
    }
}