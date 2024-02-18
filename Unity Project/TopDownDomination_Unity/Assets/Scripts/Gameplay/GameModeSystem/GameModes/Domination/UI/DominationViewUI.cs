using Gameplay.Entity.Base.Data;
using Gameplay.GameModeSystem.GameModes.Domination.Components;
using Gameplay.GameModeSystem.GameModes.Domination.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.GameModeSystem.GameModes.Domination.UI
{
    public class DominationViewUI : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private DominationViewData dominationViewData;
        
        [Header("Texts")] 
        [SerializeField] private TextMeshProUGUI dominationNameTMP;

        [Header("Images")] 
        [SerializeField] private Image dominationBackGroundImage;
        [SerializeField] private Image dominationFillImage;

        private DominationZone _displayingZone;
        
        public void Initiate(DominationZone dominationZone)
        {
            _displayingZone = dominationZone;
            dominationNameTMP.text = dominationZone.DominationZoneName;
            
            SetColors(dominationZone);
            SetCaptureFillAmount(0f);
            
            _displayingZone.OnZoneCaptureInProgress += OnZoneCaptureInProgressHandler;
            _displayingZone.OnZoneCaptured += OnZoneCapturedHandle;
        }

        private void OnDestroy()
        {
            _displayingZone.OnZoneCaptureInProgress -= OnZoneCaptureInProgressHandler;
            _displayingZone.OnZoneCaptured -= OnZoneCapturedHandle;
        }
        
        private void SetColors(DominationZone dominationZone)
        {
            if (dominationZone.EntityOwnerType == EntityType.Player)
            {
                dominationBackGroundImage.color = dominationViewData.OwnedByPlayerColor;
                dominationFillImage.color = dominationViewData.DominatedBaseColor;
            }
            else
            {
                dominationBackGroundImage.color = dominationViewData.DominatedBaseColor;
                dominationFillImage.color = dominationViewData.OwnedByPlayerColor;
            }
        }

        private void SetCaptureFillAmount(float fillAmount)
        {
            dominationFillImage.fillAmount = fillAmount;
        }
        
        private void OnZoneCaptureInProgressHandler(DominationZone dominationZone)
        {
            SetCaptureFillAmount(dominationZone.CaptureProgress);
        }

        private void OnZoneCapturedHandle(DominationZone dominationZone)
        {
            SetColors(dominationZone);
            SetCaptureFillAmount(0f);
        }
    }
}