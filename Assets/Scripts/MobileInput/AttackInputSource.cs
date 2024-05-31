using System;
using Mobile;
using UI.Joystick;
using UnityEngine;

namespace MobileInput
{
    public class AttackInputSource
    {
        private readonly ITouchable _touchable;
        private readonly OverlapPointer _overlapPointer;
        
        private float _attackTouchCooldown = 0.5f; //TODO:Move to class AttackInputSource 
        private float _attackCooldownLeft = Single.MinValue;
        private bool _isHeld;
        private IMobilable _mobileChecker;
        
        public AttackInputSource(ITouchable touchable, 
            OverlapPointer overlapPointer, IMobilable mobileChecker)
        {
            _touchable = touchable;
            _overlapPointer = overlapPointer;
            _mobileChecker = mobileChecker;
        }
        
        public void Subscribe()
        {
            _overlapPointer.Downed += OnDowned;
            _touchable.Outed += OnOuted;
        }

        public void Unsubscribe()
        {
            _overlapPointer.Downed -= OnDowned;
            _touchable.Outed -= OnOuted;
        }

        private void OnDowned()
        {
            _attackCooldownLeft = Single.MinValue;
            _isHeld = true;
        }

        private void OnOuted()
        {
            _attackCooldownLeft = Single.MinValue;
            _isHeld = false;
        }

        public bool GetInputDown()
        {
            if (_mobileChecker.IsMobile == false && _touchable.IsTouch == false)
            {
                return Input.GetButtonDown("Fire1");
            }
            
            if (_isHeld == false)
            {
                return false;
            }
            
            bool isDown = false;
            if (Time.timeSinceLevelLoad >= _attackCooldownLeft)
            {
                _attackCooldownLeft = Time.timeSinceLevelLoad + _attackTouchCooldown;
                isDown = true;
            }
            
            return isDown;
        }

        public bool GetInput()
        {
            if (_mobileChecker.IsMobile == false && _touchable.IsTouch == false)
            {
                return Input.GetButton("Fire1");
            }
            
            return _isHeld;
        }
    }
}