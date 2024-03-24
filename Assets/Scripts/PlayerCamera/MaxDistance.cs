using UnityEngine;

namespace PlayerCamera
{
    public class MaxDistance : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private float _step = 0.1f;
    
        private IScrollInputSource _inputSource;

        public float Value { get; private set; } = 1f;
        
        public static implicit operator float(MaxDistance maxDistance)
        {
            return maxDistance.Value;
        }

        private void OnValidate()
        {
            if (_inputSourceBehaviour
                && !(_inputSourceBehaviour is IScrollInputSource))
            {
                Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(IScrollInputSource));
                _inputSourceBehaviour = null;
            }
        }

        private void Awake()
        {
            _inputSource = (IScrollInputSource)_inputSourceBehaviour;
        }

        private void Update()
        {
            Value -= _inputSource.ScrollInput * _step;
            Value = Mathf.Clamp01(Value);
        }
    }
}
