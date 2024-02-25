using System;
using Gameplay.Entity.Base.Utils;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityMovement
{
    public class CharacterControllerPhysicsInteraction : MonoBehaviour
    {
        [Header("Push Force")]
        [SerializeField] private float pushPower = 2.0f;

        public event Action<bool> OnPhysicsInteractionChange; 

        public bool Active { get; private set; } = true;

        public void TogglePhysicsInteractions(bool value)
        {
            if (value == Active) return;

            Active = value;
            OnPhysicsInteractionChange?.Invoke(value);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.layer == LayerUtils.EntityLayerIndex) return;
            if (!Active) return;
            
            var body = hit.collider.attachedRigidbody;

            if (body == null || body.isKinematic) return;
            if (hit.moveDirection.y < -0.3) return;

            var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            body.velocity = pushDir * pushPower;
        }
    }
}