using Gameplay.Entity.Base.Data;
using UnityEngine;

namespace Gameplay.Entity.Base.UI
{
    public abstract class EntityViewComponent : MonoBehaviour
    {
        public EntityData EntityData { get; private set; }

        public void Initiate(EntityData entityData)
        {
            EntityData = entityData;
            OnInitiate(EntityData);
        }

        private void OnDestroy()
        {
            OnCleanUp(EntityData);
        }

        protected virtual void OnInitiate(EntityData entityData)
        {
        }
        
        protected virtual void OnCleanUp(EntityData entityData)
        {
        }
    }
}