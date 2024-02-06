using UnityEngine;

namespace Gameplay.GameInteractable.Data
{
    [CreateAssetMenu(menuName = "Entity/Data/InteractionData", fileName = "New InteractionData")]
    public class InteractData : ScriptableObject
    {
        [Header("Type")] 
        [SerializeField] private InteractType interactType;

        [Header("Info")]
        [SerializeField] private string interactInfoText = "INTERACT";
        
        [Header("Interaction Config.")] 
        [SerializeField] [Range(0.01f, 30f)] private float interactionHoldDuration = 0.01f;

        public InteractType InteractType => interactType;
        public string InteractInfoText => interactInfoText;
        public float InteractionHoldDuration => interactionHoldDuration;
    }
}