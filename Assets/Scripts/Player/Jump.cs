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
        private GroundChecker _groundChecker;
        private JumpInterval _jumpInterval;
        private bool _isJump, _isJumpDown;

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
            _groundChecker = GetComponent<GroundChecker>();
            _jumpInterval = new JumpInterval();
        }

        private void OnEnable()
        {
            _jumpInterval.Subscribe(_groundChecker);
        }

        private void OnDisable()
        {
            _jumpInterval.Unsubscribe(_groundChecker);
        }

        private void FixedUpdate()
        {
            float y = _rigidbody.velocity.y;

            if (y < 0f)
            {
                _rigidbody.velocity += Vector3.up * Physics.gravity.y * _fallRate * Time.deltaTime;
            }
            else if (y > 0.5f && InputSource.IsJumpInput == false)
            {
                _rigidbody.velocity += Vector3.up * Physics.gravity.y * _jumpRate * Time.deltaTime;
            }
        }

        private void Update()
        {
            if (InputSource.IsJumpInputDown && (_groundChecker.IsGround || _jumpInterval.IsJumpInterval))
            {
                _jumpInterval.Jump();
                _rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
                //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpSpeed, _rigidbody.velocity.z);
            }

            _isJumpDown = InputSource.IsJumpInputDown;
            _isJump = InputSource.IsJumpInput;
        }
    }
}