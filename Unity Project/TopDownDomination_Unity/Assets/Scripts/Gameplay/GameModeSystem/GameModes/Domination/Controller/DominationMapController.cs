using System.Collections.Generic;
using System.Linq;
using Gameplay.GameModeSystem.GameModes.Domination.Components;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Controller
{
    public class DominationMapController : MonoBehaviour
    {
        [Header("Spawns Config.")]
        [SerializeField] private List<DominationZone> dominationZones = new();

        [Header("Domination Environment")] 
        [SerializeField] private GameObject dominationEnvironment;
        
        public List<DominationZone> DominationZones => dominationZones;
        
        public void Initiate()
        {
            dominationEnvironment.SetActive(true);
            
            foreach (var dominationZone in dominationZones)
            {
                dominationZone.Initiate();
            }
        }

        public void EditorFindDominationZonesInMap()
        {
            if (!Application.isEditor) return;

            dominationZones = FindObjectsOfType<DominationZone>(true).ToList();
        }
    }
}