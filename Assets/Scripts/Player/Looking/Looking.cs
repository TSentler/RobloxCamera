using Player;
using Character;
using UnityEngine;

public class Looking : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Transform _yaw;
    [SerializeField] private Transform _pitch;

    private ICharacterInputSource _inputSource;
    private float _mouseX;
    private float _mouseY;
    
    private void OnValidate()
    {
        if (_inputSourceBehaviour 
            && !(_inputSourceBehaviour is ICharacterInputSource))
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _inputSourceBehaviour = null;
        }
    }

    private void Awake()
    {
        _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
    }

    private void Update()
    {
        _mouseX = _inputSource.MouseInput.x * _mouseSensitivity;
        _mouseY = _inputSource.MouseInput.y * _mouseSensitivity;

        _yaw.eulerAngles = new Vector3(0, _yaw.eulerAngles.y + _mouseX, 0);

        var angle = Vector3.SignedAngle(_yaw.forward, _pitch.forward,
            _yaw.right) - _mouseY;
        angle = Mathf.Clamp(angle, -85f, 85f);

        _pitch.localEulerAngles = new Vector3(angle, 0, 0);
    }

    private float GetRealAngle(float realAngle)
    {
        if (realAngle > 180)
        {
            realAngle -= 360;
        }

        if (Mathf.Abs(realAngle) >= 85)
        {
            realAngle = Mathf.Sign(realAngle) * 85;
        }

        return realAngle;
    }
}
