using System.Collections.Generic;
using Gameplay.Entity.Base.Data;
using Gameplay.UI.Base;
using GameWideSystems.GameDataBaseSystem.Controller;
using GameWideSystems.GameDataSystem.Controller;
using UnityEngine;

namespace Gameplay.Entity.Base.UI
{
    public class PlayerSelectionControllerUI : UIScreen
    {
        [Header("Prefab Config.")]
        [SerializeField] private PlayerEntityOptionUI entityOptionUIPrefab;
        
        [Header("Rect")] 
        [SerializeField] private RectTransform optionsRect;

        private static GameDataBaseController DataBase => GameDataBaseController.ME;
        private static GameDataController GameData => GameDataController.ME;
        private static EntityData CurrentSelectedPlayerEntity => GameData.CurrentPlayerEntityData;
        
        private readonly List<PlayerEntityOptionUI> _allPlayerEntityOptions = new();
        private PlayerEntityOptionUI _currentOptionSelected;
        
        public override void Initiate()
        {
            if (DataBase == null) return;
            
            var allEntityOptions = DataBase.EntitiesDataBase.DataItems;

            foreach (var entityData in allEntityOptions)
            {
                if (entityData.EntityType != EntityType.Player) continue;
                
                var newPlayerEntityOption = Instantiate(entityOptionUIPrefab, optionsRect);
                newPlayerEntityOption.Initiate(entityData);

                var isCurrentSelectedPlayerEntity = entityData.Id.Equals(CurrentSelectedPlayerEntity.Id);
                newPlayerEntityOption.SetSelected(isCurrentSelectedPlayerEntity);
                newPlayerEntityOption.OnPlayerEntityOptionSelected += OnPlayerOptionSelectedHandler;

                if (isCurrentSelectedPlayerEntity)
                {
                    _currentOptionSelected = newPlayerEntityOption;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var entityOptionUI in _allPlayerEntityOptions)
            {
                entityOptionUI.OnPlayerEntityOptionSelected -= OnPlayerOptionSelectedHandler;
            }
        }

        private void OnPlayerOptionSelectedHandler(PlayerEntityOptionUI entityOptionUI)
        {
            if (CurrentSelectedPlayerEntity.Id.Equals(entityOptionUI.DisplayingEntityData.Id)) return;
            
            _currentOptionSelected.SetSelected(false);
            _currentOptionSelected = entityOptionUI;
            _currentOptionSelected.SetSelected(true);
            GameData.SetPlayerEntityData(entityOptionUI.DisplayingEntityData);
        }
    }
}