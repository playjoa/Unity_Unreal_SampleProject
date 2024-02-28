using System.Collections.Generic;
using Gameplay.Entity.Base.Interfaces;
using Gameplay.GameInteractableSystem.Abstracts;
using UnityEngine;

namespace Gameplay.GameInteractableSystem.Interactables
{
    public class LightGameInteractable : GameInteractable
    {
        [Header("Lights")]
        [SerializeField] private List<Light> targetLights;
        
        protected override void OnInteractionStart(IGameEntity targetEntity)
        {
            foreach (var targetLight in targetLights)
            {
                var previousState = targetLight.enabled;
                targetLight.enabled = !previousState;
            }
        }

        protected override bool OnInteractionTickUpdate()
        {
            return false;
        }
    }
}