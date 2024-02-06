using System;
using UnityEngine;

namespace Gameplay.Entity.Base.Components
{
    [Serializable]
    public class HealthData
    {
        [Header("Health Config.")]
        [SerializeField] private bool isInvulnerable;
        
        [Header("Health Amount")]
        [SerializeField] [Range(1, 10000)] private int startHealthAmount = 100;
        [SerializeField] [Range(1, 10000)] private int maxHealthAmount = 100;
        
        public bool IsInvulnerable => isInvulnerable;
        
        public int StartHealthAmount => startHealthAmount;
        public int MaxHealthAmount => maxHealthAmount;
    }
}