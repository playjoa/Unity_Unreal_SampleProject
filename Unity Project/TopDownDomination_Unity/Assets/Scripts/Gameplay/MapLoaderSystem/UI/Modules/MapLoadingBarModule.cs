using Gameplay.MapLoaderSystem.Abstracts;
using Gameplay.MapLoaderSystem.Controller;
using Gameplay.MapLoaderSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.MapLoaderSystem.UI.Modules
{
    public class MapLoadingBarModule : MapLoadingScreenModule
    {
        [Header("Feedback Components")]
        [SerializeField] private Image slLoadingBar;
        [SerializeField] private TextMeshProUGUI txtPercentage;
        
        private const float DEFAULT_FILL_AMOUNT = 0f;
        private const string DEFAULT_PERCENTAGE_TEXT = "0%";

        public override void OnInitiated(MapData mapData)
        {
            slLoadingBar.fillAmount = DEFAULT_FILL_AMOUNT;
            txtPercentage.text = DEFAULT_PERCENTAGE_TEXT;
        }

        public override void OnUpdate(MapLoaderController mapLoader)
        {
            slLoadingBar.fillAmount = mapLoader.Progress;
            txtPercentage.text = mapLoader.ProgressPercentage;
        }
    }
}