using System;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Components
{
    public class EntityHealth : BaseEntityComponent
    {
        public event Action<HealthChangeData> OnHealthUpdate;
        public event Action<HealthChangeData> OnDied;
        
        public int MaxHealthAmount { get; private set; }
        public int CurrentHealthAmount { get; private set; }
        public float HealthPercentage => (float) CurrentHealthAmount / MaxHealthAmount;
        public bool IsInvulnerable { get; private set; } = false;
        public bool IsDead => CurrentHealthAmount <= 0;
        
        public void ToggleInvincibility(IGameEntity owner, bool value)
        {
            if (owner.Equals(Owner))
            {
                IsInvulnerable = value;
            }
        }

        public void ChangeMaxHealthAmount(int newMaxHealthAmount)
        {
            if (newMaxHealthAmount <= 0)
            {
                Debug.LogWarning($"Max Health Can't be negative or zero. Trying to set MaxHealth to: {newMaxHealthAmount} @{gameObject.name}");
                return;
            }

            MaxHealthAmount = newMaxHealthAmount;
        }
        
        public bool UpdateHealth(HealthUpdatePackageData updatePackageData)
        {
            return SetNewHealthAmount
            (
                CurrentHealthAmount + updatePackageData.Delta,
                updatePackageData
            );
        }

        protected bool SetNewHealthAmount(int newAmount, HealthUpdatePackageData updatePackageData)
        {
            if (IsInvulnerable) return false;
            if (updatePackageData.Delta == 0) return false;
            if (IsDead) return false;

            CurrentHealthAmount = Mathf.Clamp(newAmount, 0, MaxHealthAmount);
            
            var wasKill = IsDead;
            var healthEventData = new HealthChangeData
            {
                Inflicter = updatePackageData.Inflicter,
                DealtAmount = updatePackageData.Delta
            };

            OnHealthUpdate?.Invoke(healthEventData);

            if (wasKill)
            {
                OnDied?.Invoke(healthEventData);
            }

            return true;
        }
    }
}