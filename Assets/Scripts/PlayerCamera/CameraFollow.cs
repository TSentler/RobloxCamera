using UnityEngine;

namespace PlayerCamera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private ObstaclesAvoider _obstacleAvoider;
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private Transform _camera;
        [Min(0f), SerializeField] private float _minSmoothTime = 7f;
        [Min(0f), SerializeField] private float _maxSmoothTime = 19f;
        [Min(0f), SerializeField] private float _minSmoothDistance = 4f;
        [Min(0f), SerializeField] private float _maxSmoothDistance = 10f;


        private void Update()
        {
            Follow(_cameraTarget.position);
        }

        private void Follow(Vector3 _targetPosition)
        {
            if (_obstacleAvoider.CurrentLocalCameraDistance < _minSmoothDistance)
            {
                transform.position = _cameraTarget.position;
            }
            else
            {
                float smooth = GetSmooth();
                transform.position = Vector3.Slerp(transform.position,
                    _targetPosition, smooth * Time.deltaTime);
            }
        }

        private float GetSmooth()
        {
            float smooth = _minSmoothTime;
            if (_obstacleAvoider.CurrentLocalCameraDistance < _maxSmoothDistance)
            {
                float rate = (1f - _obstacleAvoider.CurrentLocalCameraDistance / _maxSmoothDistance);
                smooth = (_maxSmoothTime - _minSmoothDistance) * rate + _minSmoothTime;
            }

            return smooth;
        }
    }
}
