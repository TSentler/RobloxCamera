using System;
using UnityEngine;

namespace PlayerCamera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;
        [Min(0f), SerializeField] private float _smoothTime = 0.3f;
        
        private void LateUpdate()
        {
            Follow(_cameraTarget.position);
        }

        private void Follow(Vector3 _targetPosition)
        {
            transform.position = Vector3.Slerp(transform.position,
                _targetPosition, _smoothTime * Time.deltaTime);
        }
    }
}
