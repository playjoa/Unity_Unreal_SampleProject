using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Abstracts
{
    public abstract class BaseEntityExtraComponent : MonoBehaviour, IEntityExtraComponent
    {
        public IGameEntity Owner { get; private set; }
        
        public void Initiate(IGameEntity owner)
        {
            Owner = owner;
            OnInitiate(owner);
        }

        public void ReviveComponent()
        {
            OnRevive();
        }

        protected virtual void OnInitiate(IGameEntity owner)
        {
        }
        
        protected virtual void OnRevive()
        {
        }
    }
}