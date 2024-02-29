using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.Components.UI
{
    public class EntityHealthBarModule : EntityHealthModuleUI
    {
        [Header("Image Feedback")]
        [SerializeField] private Image healthBarImage;

        [Header("Config.")] 
        [SerializeField] private bool disableWhenDied;

        private const float ANIM_DURATION = 0.35f; 
        
        private Coroutine _animationCoroutine;

        protected override void OnInitiated(EntityHealth healthOwner)
        {
            FillHealthValue(healthOwner.HealthPercentage);
        }

        private void FillHealthValue(float healthPercent)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = StartCoroutine(AnimateSliderFill(healthPercent));
        }

        public override void OnHealthUpdate(HealthChangeData healthEventData)
        {
            FillHealthValue(healthEventData.Victim.EntityHealth.HealthPercentage);

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }

        public override void OnEntityDied(HealthChangeData healthEventData)
        {
            if (disableWhenDied)
                gameObject.SetActive(false);
        }
        
        private IEnumerator AnimateSliderFill(float targetValue)
        {
            var startValue = healthBarImage.fillAmount;
            var elapsedTime = 0f;

            while (elapsedTime < ANIM_DURATION)
            {
                elapsedTime += Time.deltaTime;
                var time = Mathf.Clamp01(elapsedTime / ANIM_DURATION);
                healthBarImage.fillAmount = Mathf.Lerp(startValue, targetValue, time);
                yield return null;
            }

            healthBarImage.fillAmount = targetValue;
        }
    }
}