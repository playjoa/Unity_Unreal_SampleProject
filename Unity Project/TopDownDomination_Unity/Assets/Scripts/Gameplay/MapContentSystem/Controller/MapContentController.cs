using System.Collections.Generic;
using Gameplay.GameModeSystem.GameModes.Domination.Controller;
using Gameplay.SpawnSystem.Abstracts;
using UnityEngine;

namespace Gameplay.MapContentSystem.Controller
{
    public class MapContentController : MonoBehaviour
    {
        [Header("Spawns Config.")]
        [SerializeField] private List<SpawnPoint> playerSpawnPoints = new();
        
        [Header("GameMode Map Controllers")] 
        [SerializeField] private DominationMapController dominationMapController;
        
        public List<SpawnPoint> PlayerSpawnPoints => playerSpawnPoints;
        
        public DominationMapController DominationMapController => dominationMapController;
    }
}