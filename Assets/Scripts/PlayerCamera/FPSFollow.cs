using System;
using UnityEngine;

namespace PlayerCamera
{
    public class FPSFollow : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTargetPoint;
        [Min(0f), SerializeField] private float _smoothTime = 0.3f,
            _rotationSmoothTime = 20f;
        
        private Quaternion _targetRotation;

        private void FixedUpdate()
        {
           // Rotate();
        }

        private void LateUpdate()
        {
            //Follow(_cameraTargetPoint.position);
            //CornerStabilization();
            //RememberTargetRotation();
        }

        private void RememberTargetRotation() => 
            _targetRotation = _cameraTargetPoint.rotation;

        private void CornerStabilization()
        {
            var parent = transform.parent;
            transform.parent = _cameraTargetPoint;
            transform.localRotation = Quaternion.Euler(
                transform.localEulerAngles.x, 0f, 0f);
            transform.parent = parent;
        }

        private void Follow(Vector3 _targetPosition)
        {
            /*
            Vector3 velocity = Vector3.zero;
            Vector3 step = Vector3.SmoothDamp(transform.position,
                _targetPosition, ref velocity, _smoothTime);
            */
            //GetComponent<Rigidbody>().MovePosition(targetPosition);
            // transform.position = Vector3.MoveTowards(transform.position,
                // _targetPosition, _smoothTime * Time.deltaTime);
            transform.position = _targetPosition;
        }

        private void Rotate()
        {
            var angle = Quaternion.Angle(_targetRotation, transform.rotation);
            if (Mathf.Abs(angle) < 0.1f)
                return;
            
            var diff = Quaternion.Inverse(transform.rotation) * _targetRotation;
            var target = transform.rotation * diff;
            
            Quaternion lerp = Quaternion.Lerp(transform.rotation, target,
                _rotationSmoothTime * Time.deltaTime);
            transform.rotation = lerp;
        }
    }
}
