using UnityEngine;

namespace Character
{
    public class JumpInterval
    {
        private readonly float _jumpInterval = 0.13f;
        
        private float _flyTime;
        private bool _isJumped;

        public bool IsJumpInterval => _isJumped == false 
                                      && Time.timeSinceLevelLoad - _flyTime < _jumpInterval;
        
        public void Subscribe(GroundChecker groundChecker)
        {
            groundChecker.Grounded += OnGrounded;
            groundChecker.Fly += OnFly;
        }
        
        public void Unsubscribe(GroundChecker groundChecker)
        {
            groundChecker.Grounded -= OnGrounded;
            groundChecker.Fly -= OnFly;
        }

        public void Jump()
        {
            _isJumped = true;
        }
        
        private void OnGrounded()
        {
            _isJumped = false;
            _flyTime = 0f;
        }

        private void OnFly()
        {
            if (_isJumped)
                return;
            
            _flyTime = Time.timeSinceLevelLoad;
        }

    }
}