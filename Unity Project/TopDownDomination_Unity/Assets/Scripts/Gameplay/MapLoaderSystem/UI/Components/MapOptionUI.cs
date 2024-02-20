using System;
using Gameplay.MapLoaderSystem.Data;
using Gameplay.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.MapLoaderSystem.Components
{
    public class MapOptionUI : MonoBehaviour
    {
        [Header("View Components")] 
        [SerializeField] private Image mapIconImage;
        [SerializeField] private Image mapSelectionImage;
        [SerializeField] private TextMeshProUGUI mapNameTMP;

        [Header("Interaction Components")] 
        [SerializeField] private Button mapSelectButton;

        [Header("Color Data")] 
        [SerializeField] private UIColorsData uiColorsData;

        public event Action<MapOptionUI> OnMapOptionSelected;

        public MapData DisplayingMapData { get; private set; }

        public void Initiate(MapData mapData)
        {
            DisplayingMapData = mapData;
            
            SetMapIconImage(DisplayingMapData);
            SetMapNameText(DisplayingMapData);
            
            mapSelectButton.onClick.AddListener(OnMapSelectClickHandler);
        }

        private void OnDestroy()
        {
            mapSelectButton.onClick.RemoveListener(OnMapSelectClickHandler);
        }

        private void SetMapIconImage(MapData mapData)
        {
            mapSelectionImage.overrideSprite = mapData.MapIcon;
        }

        private void SetMapNameText(MapData mapData)
        {
            mapNameTMP.text = mapData.MapName;
        }

        public void SetSelected(bool value)
        {
            mapSelectionImage.color = value ? uiColorsData.selectedUIColor : uiColorsData.notSelectedUIColor;
        }
        
        private void OnMapSelectClickHandler()
        {
            OnMapOptionSelected?.Invoke(this);
        }
    }
}