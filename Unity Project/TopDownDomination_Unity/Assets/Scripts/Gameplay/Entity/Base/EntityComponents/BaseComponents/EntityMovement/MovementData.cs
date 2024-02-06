using System;
using UnityEngine;

namespace Gameplay.Entity.Base.EntityComponents.BaseComponents.EntityMovement
{
    [Serializable]
    public class MovementData
    {
        [Header("Movement Config.")]
        [SerializeField] private bool canMove = true;
        
        [Header("Horizontal Speed Settings")] 
        [SerializeField] [Range(0f, 25f)] private float horizontalSpeed = 8;
        [SerializeField] [Range(2f, 10f)] private float rotateSpeed = 10;
        
        [Header("Sprint Settings")] 
        [SerializeField] [Range(1f, 3f)] private float sprintHorizontalMultiplier = 2f;
        [SerializeField] [Range(1f, 3f)] private float sprintVerticalMultiplier = 1.5f;

        [Header("Vertical Speed Settings")] 
        [SerializeField] private bool useGravity = true;
        [SerializeField] [Range(-150f, -9.8f)] private float gravitySpeed = -10;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] [Range(0.1f, 1f)] private float groundDistance = 0.5f;
        
        public const float DEFAULT_SPRINT_MULTIPLIER = 1f;
        
        public float HorizontalSpeed => horizontalSpeed;
        public float RotateSpeed => rotateSpeed;
        
        public float SprintHorizontalMultiplier => sprintHorizontalMultiplier;
        public float SprintVerticalMultiplier => sprintVerticalMultiplier;
        
        public bool UseGravity => useGravity;
        public float GravitySpeed => gravitySpeed;
        public LayerMask GroundMask => groundMask;
        public float GroundDistance => groundDistance;
        
        public bool CanMove => canMove;
    }
}