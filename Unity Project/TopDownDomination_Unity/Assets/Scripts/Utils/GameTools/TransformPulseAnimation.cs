using UnityEngine;

namespace Utils.GameTools
{
    public class TransformPulseAnimation : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform targetTransform;
        
        [Header("Animation Configuration")]
        [SerializeField] private float velocity = 5.5f;

        private float _baseSize;
        
#if UNITY_EDITOR
        private void OnValidate() => Awake();
#endif

        private void Awake()
        {
            targetTransform ??= GetComponent<Transform>();
            _baseSize = targetTransform.localScale.x;
        }

        private void OnDisable()
        {
            targetTransform.localScale = Vector3.one * _baseSize;
        }

        private void Update()
        {
            Animate();
        }

        private void Animate()
        {
            var anim = _baseSize + Mathf.Sin(Time.time * velocity) * _baseSize / 30f;
            transform.localScale = Vector3.one * anim;
        }
    }
}
