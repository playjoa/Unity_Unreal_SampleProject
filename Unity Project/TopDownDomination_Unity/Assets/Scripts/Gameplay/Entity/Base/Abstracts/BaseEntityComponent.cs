using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.Abstracts
{
    public abstract class BaseEntityComponent : MonoBehaviour, IEntityComponent
    {
        public IGameEntity Owner { get; private set; }
        
        public void Initiate(IGameEntity owner)
        {
            Owner = owner;
            OnInitiate(Owner);
        }

        public void ReviveComponent()
        {
            OnRevive();
        }

        public void Clean()
        {
            OnClean();
        }

        protected virtual void OnInitiate(IGameEntity owner)
        {
        }
        
        protected virtual void OnRevive()
        {
        }

        protected virtual void OnClean()
        {
        }
    }
}