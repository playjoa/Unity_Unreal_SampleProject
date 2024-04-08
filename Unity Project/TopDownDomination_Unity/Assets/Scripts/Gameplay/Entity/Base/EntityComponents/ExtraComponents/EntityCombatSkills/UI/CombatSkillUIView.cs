using System.Collections;
using Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.Extensions;

namespace Gameplay.Entity.Base.EntityComponents.ExtraComponents.EntityCombatSkills.UI
{
    public class CombatSkillUIView : MonoBehaviour
    {
        [Header("Images")] 
        [SerializeField] private Image skillIconImage;
        [SerializeField] private Image coolDownProgressFillImage;
        
        [Header("Text")] 
        [SerializeField] private TextMeshProUGUI skillCoolDownTimerTMP;

        private readonly WaitForSeconds _coolDownWaitTime = new(WAIT_TIME_TICK);
        
        private CombatSkill _combatSkillInDisplay;
        private Coroutine _coolDownCoroutine;

        private float TotalCoolDown => _combatSkillInDisplay.BaseData.CoolDown;
        private float CoolDownProgress => _currentCoolDownTime / TotalCoolDown;
        private bool SkillOnCoolDown => !_combatSkillInDisplay.CanCast;
        
        private float _currentCoolDownTime;
        
        private const float WAIT_TIME_TICK = 0.1f;
        
        public void Initiate(CombatSkill targetCombatSkill)
        {
            _combatSkillInDisplay = targetCombatSkill;

            SetSkillIcon(_combatSkillInDisplay.BaseData);
            ToggleCoolDownState(SkillOnCoolDown);
            
            _combatSkillInDisplay.OnSkillExecuted += OnSkillExecutedHandler;
            _combatSkillInDisplay.OnSkillCooldownReset += OnSkillCoolDownResetHandler;
            
            gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            if (_combatSkillInDisplay == null) return;
            
            _combatSkillInDisplay.OnSkillExecuted -= OnSkillExecutedHandler;
            _combatSkillInDisplay.OnSkillCooldownReset -= OnSkillCoolDownResetHandler;
        }

        private void SetSkillIcon(CombatSkillData combatSkillData)
        {
            skillIconImage.overrideSprite = combatSkillData.SkillIcon;
        }

        private void SetCoolDownTime(float remainingCoolDownTime)
        {
            skillCoolDownTimerTMP.text = remainingCoolDownTime.ToString("N1");
        }
        
        private void SetCoolDownProgress(float progress)
        {
            coolDownProgressFillImage.fillAmount = 1 - progress;
        }

        private void ToggleCoolDownState(bool coolDownState)
        {
            if (coolDownState)
            {
                skillCoolDownTimerTMP.gameObject.SetActive(true);
                coolDownProgressFillImage.gameObject.SetActive(true);
            }
            else
            {
                skillCoolDownTimerTMP.gameObject.SetActive(false);
                coolDownProgressFillImage.gameObject.SetActive(false);
            }
        }

        private IEnumerator CoolDownCoroutine()
        {
            while (SkillOnCoolDown)
            {
                yield return _coolDownWaitTime;
                
                _currentCoolDownTime += WAIT_TIME_TICK;
                SetCoolDownTime(_currentCoolDownTime);
                SetCoolDownProgress(CoolDownProgress);
            }

            ToggleCoolDownState(false);
            _coolDownCoroutine = null;
        }

        private void OnSkillExecutedHandler(CombatSkill combatSkill)
        {
            _currentCoolDownTime = 0;
            SetCoolDownTime(_currentCoolDownTime);
            SetCoolDownProgress(CoolDownProgress);
            ToggleCoolDownState(true);
            
            _coolDownCoroutine = StartCoroutine(CoolDownCoroutine());
        }
        
        private void OnSkillCoolDownResetHandler(CombatSkill combatSkill)
        {
            if (_coolDownCoroutine == null) return;
            
            StopCoroutine(_coolDownCoroutine);
            ToggleCoolDownState(false);
            _coolDownCoroutine = null;
        }
    }
}