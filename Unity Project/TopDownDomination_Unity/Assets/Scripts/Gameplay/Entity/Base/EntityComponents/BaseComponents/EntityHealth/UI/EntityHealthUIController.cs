using Gameplay.Entity.Base.Interfaces;
using Gameplay.UI.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Components.UI
{
    public class EntityHealthUIController : MonoBehaviour, IPlayerUI
    {
        [Header("Starting IHealth Component (Optional)")] 
        [SerializeField] private EntityHealth startingHealthComponent;
        
        [Header("UI Modules")]
        [SerializeField] private EntityHealthModuleUI[] healthUIModules;
        
        private EntityHealth _ownerHealth;
        
         private void OnDestroy()
        {
            UnsubscribePreviousOwner();
        }
        
        public void Initiate(IGameEntity playerEntity)
        {
            InitiateUIController(playerEntity.EntityHealth);
        }

        public void ToggleUI(bool valueToSet)
        {
            gameObject.SetActive(valueToSet);
        }

        public void UpdateChildModules()
        {
            if (!Application.isEditor) return;

            healthUIModules = GetComponentsInChildren<EntityHealthModuleUI>();
        }

        private void InitiateUIController(EntityHealth iHealth)
        {
            if (iHealth == null) return;

            UnsubscribePreviousOwner();
            _ownerHealth = iHealth;
            
            foreach (var healthUIModule in healthUIModules)
            {
                healthUIModule.Initiate(iHealth);
            }
            
            _ownerHealth.OnHealthUpdate += OnHealthUpdatedHandler;
            _ownerHealth.OnDied += OnDiedHandler;
        }

        private void UnsubscribePreviousOwner()
        {
            if (_ownerHealth == null) return;
             
            _ownerHealth.OnHealthUpdate -= OnHealthUpdatedHandler;
            _ownerHealth.OnDied -= OnDiedHandler;
        }
        
        private void OnHealthUpdatedHandler(HealthChangeData healthEventData)
        {
            foreach (var healthUIModule in healthUIModules)
            {
                healthUIModule.OnHealthUpdate(healthEventData);
            }
        }
        
        private void OnDiedHandler(HealthChangeData healthEventData)
        {            
            foreach (var healthUIModule in healthUIModules)
            {
                healthUIModule.OnEntityDied(healthEventData);
            }
        }
    }
}