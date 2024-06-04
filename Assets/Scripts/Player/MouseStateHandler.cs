using PointerLock.Hook;
using UnityEngine;

namespace Player
{
    public class MouseStateHandler : MonoBehaviour
    {
        private PointerLockHook _pointerLockHook;
        private InputSetter _inputSetter;

        public bool IsMouseEnable { get; private set; } // Must always false in mobile 

        private void Awake()
        {
            _pointerLockHook = FindObjectOfType<PointerLockHook>();
            _inputSetter = FindObjectOfType<InputSetter>();
        }

        private void OnEnable()
        {
            _pointerLockHook.PointerLocked += DisableMouse;
            _pointerLockHook.PointerUnlocked += EnableMouse;
        }

        private void OnDisable()
        {
            _pointerLockHook.PointerLocked -= DisableMouse;
            _pointerLockHook.PointerUnlocked -= EnableMouse;
        }

        public void DisableMouse()
        {
            if (_inputSetter.IsMobile)
                return;

            IsMouseEnable = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void EnableMouse()
        {
            if (_inputSetter.IsMobile)
                return;

            IsMouseEnable = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}