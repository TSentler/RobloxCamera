using Mobile;
using UI.Joystick;
using UnityEngine;

namespace MobileInput
{
    public class JumpInputSource
    {
        private readonly ButtonPointer _buttonPointer;
        
        private bool _isJump;
        private IMobilable _mobileChecker;


        public JumpInputSource(ButtonPointer buttonPointer,
            IMobilable mobileChecker)
        {
            _buttonPointer = buttonPointer;
            _mobileChecker = mobileChecker;
        }
        
        public void Subscribe()
        {
            _buttonPointer.Downed += OnDowned;
        }

        public void Unsubscribe()
        {
            _buttonPointer.Downed -= OnDowned;
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

        public bool GetInput()
        {
            if (_mobileChecker.IsMobile == false && _buttonPointer.IsTouch == false)
            {
                return Input.GetButton("Jump");
            }

            return _buttonPointer.IsTouch;
        }
    }
}