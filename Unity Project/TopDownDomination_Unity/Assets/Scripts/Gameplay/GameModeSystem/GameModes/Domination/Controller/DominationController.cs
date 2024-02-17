using System.Collections.Generic;
using Gameplay.GameModeSystem.GameModes.Domination.Components;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Controller
{
    public class DominationController : MonoBehaviour
    {
        [Header("Spawns Config.")]
        [SerializeField] private List<DominationZone> dominationZones = new();

        public List<DominationZone> DominationZones => dominationZones;
        
        public void Initiate()
        {
            foreach (var dominationZone in dominationZones)
            {
                dominationZone.Initiate();
            }
        }
    }
}