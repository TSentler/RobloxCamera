using PointerLock.Hook;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IMouseActivator { }

    public class MouseStateHandler : MonoBehaviour
    {
        private readonly List<IMouseActivator> _mouseActivators = new List<IMouseActivator>();

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

        public void DisableMouse(IMouseActivator mouseActivator)
        {
            if (_inputSetter.IsMobile)
                return;

            _mouseActivators.Remove(mouseActivator);

            if (_mouseActivators.Count > 0)
                return;

            DisableMouse();
        }

        public void EnableMouse(IMouseActivator mouseActivator)
        {
            if (_inputSetter.IsMobile)
                return;

            if (_mouseActivators.Contains(mouseActivator) == false)
            {
                _mouseActivators.Add(mouseActivator);
            }

            EnableMouse();
        }

        private void DisableMouse()
        {
            IsMouseEnable = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void EnableMouse()
        {
            IsMouseEnable = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}