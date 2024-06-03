using Character;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private float _jumpSpeed = 5f;
        [SerializeField] private float _fallRate = 1.5f;
        [SerializeField] private float _jumpRate = 1f;

        private ICharacterInputSource InputSource;
        private Rigidbody _rigidbody;

        private void OnValidate()
        {
            if (_inputSourceBehaviour
                && !(_inputSourceBehaviour is ICharacterInputSource))
            {
                Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
                _inputSourceBehaviour = null;
            }
        }

        private void Initialize(ICharacterInputSource inputSource)
        {
            if (InputSource == null)
            {
                InputSource = inputSource;
            }
        }

        protected void Awake()
        {
            Initialize((ICharacterInputSource)_inputSourceBehaviour);
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            float y = _rigidbody.velocity.y;
            if (InputSource.IsJumpInputDown)
            {
                _rigidbody.velocity = Vector3.up * _jumpSpeed;
            }

            if (y < 0f)
            {
                _rigidbody.velocity += Vector3.up * Physics.gravity.y * _fallRate * Time.deltaTime;
            }
            else if (y > 0f && InputSource.IsJumpInput == false)
            {
                _rigidbody.velocity += Vector3.up * Physics.gravity.y * _jumpRate * Time.deltaTime;
            }
        }
    }
}