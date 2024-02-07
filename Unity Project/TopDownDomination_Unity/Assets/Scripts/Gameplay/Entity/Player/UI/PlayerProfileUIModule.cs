using Gameplay.Entity.Base.Interfaces;
using Gameplay.UI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Player.UI
{
    public class PlayerProfileUIModule : MonoBehaviour, IPlayerUI
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI playerEntityNameTMP;

        [Header("Image")] 
        [SerializeField] private Image playerEntityIconImage;
        
        public void Initiate(IGameEntity gameController)
        {
            SetEntityNameText(gameController.EntityData.EntityName);
            SetEntityImage(gameController.EntityData.EntityIcon);
        }

        public void ToggleUI(bool valueToSet)
        {
            gameObject.SetActive(valueToSet);
        }

        private void SetEntityNameText(string entityName)
        {
            playerEntityNameTMP.text = entityName;
        }

        private void SetEntityImage(Sprite entityIconSprite)
        {
            playerEntityIconImage.overrideSprite = entityIconSprite;
        }
    }
}