using System;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractable.Data;
using Gameplay.GameInteractable.Interfaces;
using UnityEngine;

namespace Gameplay.GameInteractable.Abstracts
{
    public abstract class GameInteractable : MonoBehaviour, IGameInteractable
    {
        [Header("Base GameInteractable SetUp")]
        [SerializeField] private InteractData startInteractionData;
        
        public event Action OnStartInteraction;
        public event Action OnEndInteraction;
        
        public InteractData InteractData { get; private set; }
        public IGameEntity CurrentInteractingGameEntity { get; private set; }
        
        public bool Interacting { get; private set; }
        public virtual bool CanInteract => true;

        private void Awake()
        {
            if (startInteractionData) SetInteractionData(startInteractionData);
            
            OnInitiate();
        }

        protected virtual void OnInitiate()
        {
        }

        public void SetInteractionData(InteractData newInteractionData)
        {
            InteractData = newInteractionData;
        }

        public bool TryStartInteraction(IGameEntity gameEntity)
        {
            if (!InteractValidation(gameEntity)) return false;

            OnInteractionStart(gameEntity);
            Interacting = true;
            CurrentInteractingGameEntity = gameEntity;
            OnStartInteraction?.Invoke();
            return true;
        }

        public bool InteractionUpdateTick()
        {
            return OnInteractionTickUpdate();
        }

        public void EndInteraction()
        {
            OnInteractionEnd(CurrentInteractingGameEntity);
            CurrentInteractingGameEntity = null;
            Interacting = false;
            OnEndInteraction?.Invoke();
        }

        protected virtual bool InteractValidation(IGameEntity targetEntity)
        {
            return true;
        }

        protected abstract void OnInteractionStart(IGameEntity targetEntity);

        protected abstract bool OnInteractionTickUpdate();

        protected virtual void OnInteractionEnd(IGameEntity targetEntity)
        {
        }
    }
}