using Gameplay.GameVfxSystem.Components;
using UnityEngine;
using Utils.UniqueId.Components;

namespace Gameplay.GameVfxSystem.Data
{
    [CreateAssetMenu(menuName = "VFX/VFX Data", fileName = "New VFX Data")]
    public class VfxData : ScriptableObjectWithId
    {
        [Header("Prefab")]
        [SerializeField] private GameVfx vfxPrefab;
        
        [Header("VFX Config.")]
        [SerializeField] private float vfxDuration = 1;
        [SerializeField] private VfxType type;
        [SerializeField] private Vector3 startPositionOffSet;
        
        public GameVfx VfxPrefab => vfxPrefab;
        
        public float VfxDuration => vfxDuration;
        public VfxType Type => type;
        public Vector3 StartPositionOffSet => startPositionOffSet;
    }
}