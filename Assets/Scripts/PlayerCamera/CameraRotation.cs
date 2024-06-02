using Character;
using UnityEngine;

namespace PlayerCamera
{
    public class CameraRotation : MonoBehaviour
    {
        private readonly float _pitchLimit = 85f;

        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        [SerializeField] private Transform _cameraRoot, _pitch, _yaw, _camera;
        [SerializeField] private Vector2 _turn;
        [Min(0.001f), SerializeField] private float _smoothDamp = 10f;

        private ICharacterInputSource InputSource;

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
        
        protected virtual void Awake()
        {
            Initialize((ICharacterInputSource)_inputSourceBehaviour);
        }

        private void LateUpdate()
        {
            UpdateMouseInput();
            RotateCamera();
        }

        private void UpdateMouseInput()
        {
            _turn += InputSource.MouseInput;
            _turn.x %= 360f;
            _turn.y %= 360f;
            _turn.y = ClampAngle(_turn.y);
        }

        private void RotateCamera()
        {
            float speed = _smoothDamp * Time.deltaTime;
            //Vector2 turn = InputSource.MouseInput;
            Vector3 turnX = new Vector3(0f, _turn.x, 0f);
            Vector3 turnY = new Vector3(-_turn.y, 0f, 0f);
            Quaternion targetX = Quaternion.Euler(turnX);
            Quaternion targetY = Quaternion.Euler(turnY);

            _yaw.localRotation = Quaternion.Slerp(_yaw.localRotation, targetX, speed);
            
            Quaternion pitch = Quaternion.Slerp(_pitch.localRotation, targetY, speed);
            float pitchAngle = Quaternion.Angle(_yaw.rotation, _yaw.rotation * pitch);
            _pitch.localRotation = pitch;
            //_pitch.localRotation = ClampQuaternion(pitch, _pitchLimit);
            
            //Debug.Log(Quaternion.Angle(Quaternion.identity, pitch));
            //Debug.Log(Quaternion.Angle(_yaw.rotation, _pitch.rotation));
        }

        private float ClampAngle(float realAngle)
        {
            if (Mathf.Abs(realAngle) >= _pitchLimit)
            {
                realAngle = Mathf.Sign(realAngle) * _pitchLimit;
            }

            return realAngle;
        }

        private Quaternion ClampQuaternion(Quaternion target, float maxAngle) =>
            Quaternion.RotateTowards(Quaternion.identity, target, maxAngle);

    }
}
