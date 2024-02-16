using System.Collections.Generic;
using Gameplay.Entity.Base.Data;
using UnityEngine;

namespace Gameplay.GameModeSystem.GameModes.Domination.Data
{
    [CreateAssetMenu(menuName = "GameSystems/GameModes/Domination/DominationZoneData", fileName = "New DominationZoneData")]
    public class DominationZoneData : ScriptableObject
    {
        [Header("Meta Data")]
        [SerializeField] private string dominationZoneName = "A";
        [SerializeField] private float captureTime = 10f;

        [Header("Guardians Config.")] 
        [SerializeField] private List<EntityData> guardiansData = new();
        
        public string DominationZoneName => dominationZoneName;
        public float CaptureTime => captureTime;

        public List<EntityData> GuardiansData => guardiansData;
    }
}