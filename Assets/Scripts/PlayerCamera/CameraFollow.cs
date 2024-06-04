using System;
using UnityEngine;

namespace PlayerCamera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private Transform _camera;
        [Min(0f), SerializeField] private float _smoothTime = 6f;
        [Min(0f), SerializeField] private float _minDistance = 0.3f;
        
        private void Update()
        {
            Follow(_cameraTarget.position);
        }

        private void Follow(Vector3 _targetPosition)
        {
            float distance = (_cameraTarget.position - _camera.position).magnitude;
            if (distance < _minDistance)
            {
                transform.position = _cameraTarget.position;
            }
            else
            {
                transform.position = Vector3.Slerp(transform.position,
                    _targetPosition, _smoothTime * Time.deltaTime);
            }
        }
    }
}
