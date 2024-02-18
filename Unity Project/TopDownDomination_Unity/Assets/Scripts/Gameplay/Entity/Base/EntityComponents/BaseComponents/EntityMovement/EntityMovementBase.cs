﻿using System;
using Gameplay.Entity.Base.Abstracts;
using Gameplay.Entity.Base.Interfaces;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityMovement
{
    public class EntityMovementBase : BaseEntityComponent
    {
        [Header("Target Transforms")]
        [SerializeField] protected Transform groundCheckerTransform;
        
        public event Action<bool> OnGroundStateChanged;
        
        public virtual bool MovementActive { get; protected set; }
        
        public virtual bool GravityActive => movementData.UseGravity;
        public bool Grounded { get; protected set; }
        public bool Sprinting { get; protected set; }
        
        public virtual Vector3 EntityMovementDirection => Vector3.zero;
        public virtual Vector3 EntityGadgetAimDirection => Vector3.zero;

        public IGameEntity Owner { get; private set; }
        
        protected MovementData movementData;
        
        protected bool RotationActive { get; private set; } = true;

        protected override void OnInitiate(IGameEntity owner)
        {
            movementData = owner.EntityData.MovementData;
        }

        public void ReviveComponent(IGameEntity reviver)
        {
            
        }

        public virtual float GetHorizontalSpeed()
        {
            return 0;
        }

        public virtual void ToggleMovement(bool value)
        {
            MovementActive = value;
        }

        public void ToggleRotation(bool value)
        {
            RotationActive = value;
        }

        public virtual void Teleport(Vector3 teleportPosition, float teleportDuration = 0)
        {
            Invoke(nameof(InvokeTeleport), teleportDuration);
            return;

            void InvokeTeleport()
            {
                transform.position = teleportPosition;
            }
        }
        
        private void Update()
        {
            if (!Owner.IsActive) return;
            OnUpdate();
        }

        private void LateUpdate()
        {
            OnLateUpdate();
            RotateCharacter();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }

        protected virtual void OnUpdate() { }
        
        protected virtual void OnLateUpdate() { }
        
        protected virtual void OnFixedUpdate() { }

        protected void InvokeGroundedEvent(bool state) => OnGroundStateChanged?.Invoke(state);

        private void RotateCharacter()
        {
            if (!RotationActive) return;
            if (Owner.EntityHealth.IsDead) return;

            var lookDirection = GetUnitAimDirection();
            var lookRotation = Quaternion.LookRotation(lookDirection);
            var slerpRotation = Quaternion.Slerp(Owner.EntityGraphics.EntityGraphicsHolder.rotation, lookRotation,
                Time.deltaTime * movementData.RotateSpeed);

            Owner.EntityGraphics.EntityGraphicsHolder.rotation = slerpRotation;
        }

        // TODO - Get aim rotation
        private Vector3 GetUnitAimDirection()
        {
            return Vector3.zero;
            /*var aimDirection = Owner.EntityWeaponController.EntityTargetFinder.AimDirection;
            return new Vector3(aimDirection.x, 0, aimDirection.y);*/
        }
    }
}