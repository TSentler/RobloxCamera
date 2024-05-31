using UI.Joystick;
using UnityEngine;

namespace MobileInput
{
    public class JumpInputSource
    {
        private readonly ClickPointer _clickPointer;
        
        private bool _isJump;
        
        public JumpInputSource(ClickPointer clickPointer)
        {
            _clickPointer = clickPointer;
        }
        
        public void Subscribe()
        {
            _clickPointer.Downed += OnDowned;
        }

        public void Unsubscribe()
        {
            _clickPointer.Downed -= OnDowned;
        }

        private void OnDowned()
        {
            _isJump = true;
        }

        public bool GetInputDown()
        {
            var input = Input.GetButtonDown("Jump") || _isJump;
            _isJump = false;
            return input;
        }
    }
}