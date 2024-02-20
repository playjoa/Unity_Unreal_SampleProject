using System;
using Gameplay.Entity.Base.Data;
using Gameplay.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.UI
{
    public class PlayerEntityOptionUI : MonoBehaviour
    {
        [Header("View Components")] 
        [SerializeField] private Image playerIconImage;
        [SerializeField] private Image playerSelectionImage;
        [SerializeField] private TextMeshProUGUI playerNameTMP;

        [Header("Interaction Components")] 
        [SerializeField] private Button playerSelectButton;

        [Header("Color Data")] 
        [SerializeField] private UIColorsData uiColorsData;

        public event Action<PlayerEntityOptionUI> OnPlayerEntityOptionSelected;

        public EntityData DisplayingMapData { get; private set; }

        public void Initiate(EntityData mapData)
        {
            DisplayingMapData = mapData;
            
            SetMapIconImage(DisplayingMapData);
            SetMapNameText(DisplayingMapData);
            
            playerSelectButton.onClick.AddListener(OnPLayerSelectClickHandler);
        }

        private void OnDestroy()
        {
            playerSelectButton.onClick.RemoveListener(OnPLayerSelectClickHandler);
        }

        private void SetMapIconImage(EntityData mapData)
        {
            playerIconImage.overrideSprite = mapData.EntityIcon;
        }

        private void SetMapNameText(EntityData mapData)
        {
            playerNameTMP.text = mapData.EntityName;
        }

        public void SetSelected(bool value)
        {
            playerSelectionImage.color = value ? uiColorsData.selectedUIColor : uiColorsData.notSelectedUIColor;
        }
        
        private void OnPLayerSelectClickHandler()
        {
            OnPlayerEntityOptionSelected?.Invoke(this);
        }
    }
}