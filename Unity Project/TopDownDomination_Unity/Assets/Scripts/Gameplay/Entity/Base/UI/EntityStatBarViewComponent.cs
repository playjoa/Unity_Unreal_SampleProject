using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.UI
{
    public class EntityStatBarViewComponent : EntityViewComponent
    {
        [Header("Image Bar")]
        [SerializeField] private Image statFillBarImage;

        protected void SetStatFillAmount(float fillAmount)
        {
            statFillBarImage.fillAmount = fillAmount;
        }
    }
}