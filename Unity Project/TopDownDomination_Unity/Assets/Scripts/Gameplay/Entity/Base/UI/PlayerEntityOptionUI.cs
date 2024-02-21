using System;
using Gameplay.Entity.Base.Data;
using Gameplay.UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.UI
{
    public class PlayerEntityOptionUI : MonoBehaviour
    {
        [Header("Feedback Components")] 
        [SerializeField] private Image playerSelectionImage;
        
        [Header("View Components")] 
        [SerializeField] private EntityViewComponent[] entityViewComponents = Array.Empty<EntityViewComponent>();

        [Header("Interaction Components")] 
        [SerializeField] private Button playerSelectButton;

        [Header("Color Data")] 
        [SerializeField] private UIColorsData uiColorsData;

        public event Action<PlayerEntityOptionUI> OnPlayerEntityOptionSelected;

        public EntityData DisplayingEntityData { get; private set; }

        public void Initiate(EntityData entityData)
        {
            DisplayingEntityData = entityData;

            foreach (var entityViewComponent in entityViewComponents)
            {
                entityViewComponent.Initiate(entityData);
            }
            
            playerSelectButton.onClick.AddListener(OnPLayerSelectClickHandler);
        }

        private void OnDestroy()
        {
            playerSelectButton.onClick.RemoveListener(OnPLayerSelectClickHandler);
        }
        
        public void UpdateChildModules()
        {
            if (!Application.isEditor) return;

            entityViewComponents = GetComponentsInChildren<EntityViewComponent>();
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