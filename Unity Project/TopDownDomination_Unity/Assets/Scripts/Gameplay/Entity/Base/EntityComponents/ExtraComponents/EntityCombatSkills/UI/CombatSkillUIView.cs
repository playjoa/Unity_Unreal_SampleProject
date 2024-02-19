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
        
        private const float ON_READY_ALPHA = 1f;
        private const float ON_COOLDOWN_ALPHA = 0.6f;
        private const float WAIT_TIME_TICK = 0.1f;
        
        public void Initiate(CombatSkill targetCombatSkill)
        {
            _combatSkillInDisplay = targetCombatSkill;

            SetSkillIcon(_combatSkillInDisplay.BaseData);
            ToggleCoolDownState(SkillOnCoolDown);
            
            _combatSkillInDisplay.OnSkillExecuted += OnSkillExecutedHandler;
            _combatSkillInDisplay.OnSkillCooldownReset += OnSkillCoolDownResetHandler;
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
            skillCoolDownTimerTMP.text = remainingCoolDownTime.ToNiceTimer();
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
                skillIconImage.SetAlpha(ON_COOLDOWN_ALPHA);
            }
            else
            {
                skillCoolDownTimerTMP.gameObject.SetActive(false);
                skillIconImage.SetAlpha(ON_READY_ALPHA);
            }
        }

        private IEnumerator CoolDownCoroutine()
        {
            while (SkillOnCoolDown)
            {
                yield return _coolDownWaitTime;
                
                _currentCoolDownTime -= WAIT_TIME_TICK;
                SetCoolDownTime(_currentCoolDownTime);
                SetCoolDownProgress(CoolDownProgress);
            }

            ToggleCoolDownState(false);
            _coolDownCoroutine = null;
        }

        private void OnSkillExecutedHandler(CombatSkill combatSkill)
        {
            _currentCoolDownTime = combatSkill.BaseData.CoolDown;
            SetCoolDownTime(_currentCoolDownTime);
            SetCoolDownProgress(CoolDownProgress);

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