using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Components.UI
{
    public abstract class EntityHealthModuleUI : MonoBehaviour
    {
        protected IGameEntity Owner { get; private set; }
        protected EntityHealth HealthStats { get; private set; }
        
        public void Initiate(EntityHealth health)
        {
            Owner = health.Owner;
            HealthStats = health;
            OnInitiated(HealthStats);
        }

        protected abstract void OnInitiated(EntityHealth owner);
        
        public virtual void OnHealthUpdate(HealthChangeData healthEventData) { }
        
        public virtual void OnEntityDied(HealthChangeData healthEventData) { }
    }
}