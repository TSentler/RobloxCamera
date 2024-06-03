using UnityEngine;

namespace Player
{
    public class PlatformerMovement : Movement
    {
        [SerializeField] private Transform _cameraRoot;
        [SerializeField] private float _speed = 500f, _torque = 10f;

        private Vector2 _inputDirection;
        private Quaternion _targetRotation;

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Update()
        {
            SetDirection(InputSource.MovementInput);
        }

        private void Move()
        {
            float y = Rigidbody.velocity.y;

            Vector3 moveDirection =
                            new Vector3(_inputDirection.x, 0f, _inputDirection.y);
            moveDirection = Quaternion.AngleAxis(_cameraRoot.eulerAngles.y, Vector3.up)
                * moveDirection;
            var path = moveDirection * _speed;
            Rigidbody.velocity = moveDirection * _speed * Time.deltaTime; // * Time.deltaTime;
            Rigidbody.velocity += Vector3.up * y;
            return;

            //Rigidbody.MovePosition(transform.position + path * Time.deltaTime);
            //Rigidbody.AddForce(path/Time.deltaTime/Time.deltaTime, ForceMode.Force);
            //

            

            if (moveDirection.sqrMagnitude == 0f)
            {
                //Rigidbody.velocity = Vector3.up * y;
            }

            float currentSpeed = Rigidbody.velocity.magnitude;
            float maxSpeed = _speed * Time.deltaTime * 2f;
            float actualForce = _speed * (1 - currentSpeed / maxSpeed);
            //Rigidbody.AddForce(moveDirection * actualForce);
        }

        private void Rotate()
        {
            if (_inputDirection.sqrMagnitude != 0f)
            {
                _targetRotation = _cameraRoot.rotation;
            }

            Quaternion torque = Quaternion.Slerp(Rigidbody.rotation, _targetRotation, _torque * Time.deltaTime);
            torque *= Quaternion.Inverse(Rigidbody.rotation);
            torque.ToAngleAxis(out float angle, out Vector3 axis);
            Vector3 angular = angle * axis;
            Rigidbody.angularVelocity = angular;
            //Rigidbody.AddTorque(torque.eulerAngles);
            //Rigidbody.MoveRotation(torque);
        }

        public override void SetDirection(Vector2 direction)
        {
            if (CanMove == false)
            {
                direction = Vector2.zero;
            }
            _inputDirection = direction;
            base.SetDirection(_inputDirection);
        }
    }
}
