using UnityEngine;

namespace Utils.Animations
{
    public class TransformObjectRotatorAnimation : MonoBehaviour
    {
        [Header("Rotation State")] 
        [SerializeField] private bool rotationActive = true;
        
        [Header("Target Transform")]
        [SerializeField] private Transform targetTransform;

        [Header("Axis Configuration")] 
        [SerializeField] private bool rotateX = false;
        [SerializeField] private bool rotateY = false;
        [SerializeField] private bool rotateZ = true;
        
        [Header("Speed Configuration")]
        [SerializeField] private float rotationVelocity = 300f;
        
        private const float DEFAULT_ROTATE_VALUE = 0;
        private bool Rotating => rotateX || rotateY || rotateZ;
        
#if UNITY_EDITOR
        private void OnValidate() => GetTransformIfNotSet();
#endif

        private void Awake()
        {
            GetTransformIfNotSet();
        }

        private void Update()
        {
            if (!rotationActive) return;
            
            RotateObject();
        }

        public void ToggleRotationState(bool value)
        {
            rotationActive = value;
        }

        private void GetTransformIfNotSet()
        {
            targetTransform ??= GetComponent<Transform>();
        }

        private void RotateObject()
        {
            if (!Rotating) return;
            
            var easedVelocity = rotationVelocity * Time.deltaTime;
            
            if (rotateX)
                RotateTransform(new Vector3(easedVelocity, DEFAULT_ROTATE_VALUE, DEFAULT_ROTATE_VALUE));

            if (rotateY)
                RotateTransform(new Vector3(DEFAULT_ROTATE_VALUE, easedVelocity, DEFAULT_ROTATE_VALUE));

            if (rotateZ)
                RotateTransform(new Vector3(DEFAULT_ROTATE_VALUE, DEFAULT_ROTATE_VALUE, easedVelocity));
        }

        private void RotateTransform(Vector3 rotationToSet) => targetTransform.Rotate(rotationToSet, Space.Self);
    }
}