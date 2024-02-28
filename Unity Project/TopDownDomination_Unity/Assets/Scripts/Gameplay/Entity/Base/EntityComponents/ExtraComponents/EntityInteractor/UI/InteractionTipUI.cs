using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityInteractor.UI
{
    public class InteractionTipUI : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI interactInfoTMP;

        [Header("Images")] 
        [SerializeField] private Image interactProgressSlider;
        
        [Header("Other")]
        [SerializeField] private GameObject progressSliderHolder;
        
        private const float FULL_INTERACT_VALUE = 1f;
        private const float EMPTY_INTERACT_VALUE = 0f;
        private const float CLEAR_INTERACTION_DURATION = 0.25f;
        
        private Coroutine _interactCoroutine;
        
        public void Toggle(bool value)
        {
            gameObject.SetActive(value);

            if (!value)
            {
                ToggleSliderProgress(false);
                interactProgressSlider.fillAmount = EMPTY_INTERACT_VALUE;
            }
        }
        
        public void ToggleSliderProgress(bool value)
        {
            progressSliderHolder.SetActive(value); 
        }

        public void SetInteractionText(string textInfo)
        {
            interactInfoTMP.text = textInfo;
        }

        public void StartInteractSliderAnimation(float timer)
        {
            if (_interactCoroutine != null)
                StopCoroutine(_interactCoroutine);

            if (!gameObject.activeSelf) return;
            _interactCoroutine = StartCoroutine(AnimateSliderCoroutine(timer));
        }

        public void CancelInteractSliderAnimation()
        {
            if (_interactCoroutine != null)
                StopCoroutine(_interactCoroutine);

            if (!gameObject.activeSelf) return;
            _interactCoroutine = StartCoroutine(ClearSliderCoroutine());
        }

        private IEnumerator AnimateSliderCoroutine(float timer)
        {
            var elapsedTime = 0f;
            var initialFillAmount = interactProgressSlider.fillAmount;

            while (elapsedTime < timer)
            {
                elapsedTime += Time.deltaTime;
                interactProgressSlider.fillAmount = Mathf.Lerp(initialFillAmount, FULL_INTERACT_VALUE, elapsedTime / timer);
                yield return null;
            }
        }

        private IEnumerator ClearSliderCoroutine()
        {
            var elapsedTime = 0f;
            var initialFillAmount = interactProgressSlider.fillAmount;

            while (elapsedTime < CLEAR_INTERACTION_DURATION)
            {
                elapsedTime += Time.deltaTime;
                interactProgressSlider.fillAmount = Mathf.Lerp(initialFillAmount, EMPTY_INTERACT_VALUE, elapsedTime / CLEAR_INTERACTION_DURATION);
                yield return null;
            }
        }
    }
}