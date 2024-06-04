using UnityEngine;
using UnityEngine.Events;

namespace Character 
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Transform _groundPoint;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _groundOverlapRadius = 0.15f;

        private bool _isGrounded;

        public event UnityAction Grounded, Fly;
        
        public bool IsGround { get; private set; }

        private void Update()
        {
            IsGround = GroundCheck();
            
            if (IsGround)
            {
                GroundedNowCheck();
            }
            else
            {
                FlyNowCheck();
            }
        }

        private bool GroundCheck()
        {
            var hitColliders = new Collider[1];
            hitColliders = Physics.OverlapSphere(_groundPoint.position, 
                _groundOverlapRadius, _groundMask);
            return hitColliders.Length > 0;
        }


        private void FlyNowCheck()
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                Fly?.Invoke();
            }
        }

        private void GroundedNowCheck()
        {
            if (_isGrounded == false)
            {
                _isGrounded = true;
                Grounded?.Invoke();
            }
        }
    }
}
