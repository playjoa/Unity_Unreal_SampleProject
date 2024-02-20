using Gameplay.Entity.Base.Data;
using GameWideSystems.GameDataSystem.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.UI
{
    public class PlayerCharacterBannerView : MonoBehaviour
    {
        [Header("View Components")] 
        [SerializeField] private Image playerIconImage;
        [SerializeField] private TextMeshProUGUI playerNameTMP;
        
        private static GameDataController GameData => GameDataController.ME;
        
        private void Awake()
        {
            if (GameData == null) return;

            SetPlayerEntityView(GameData.CurrentPlayerEntityData);

            GameData.OnCurrentPlayerEntityDataSaved += OnPlayerEntityDataChangedHandler;
        }

        private void OnDestroy()
        {
            if (GameData == null) return;
            
            GameData.OnCurrentPlayerEntityDataSaved -= OnPlayerEntityDataChangedHandler;
        }

        private void SetPlayerEntityView(EntityData entityData)
        {
            playerIconImage.overrideSprite = entityData.EntityIcon;
            playerNameTMP.text = entityData.EntityName;
        }
        
        private void OnPlayerEntityDataChangedHandler(EntityData entityData)
        {
            SetPlayerEntityView(entityData);
        }
    }
}