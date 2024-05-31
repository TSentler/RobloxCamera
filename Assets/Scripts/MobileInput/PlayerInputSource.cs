using PointerLock.Hook;
using UI.Joystick;
using UnityEngine;

namespace MobileInput
{
    public class PlayerInputSource : MonoBehaviour, ICharacterInputSource
    {
        private PointerLockHook _pointerLockHook;
        private CursorLockerPanel _lockerPanel;
        private MovementInputSource _movementInput;
        private RotationInputSource _rotationInput;
        private ZoomInputSource _zoomInput;
        private bool _isPause, _isUnlocked;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseInput { get; private set; }
        public float ScrollInput { get; private set; }
        
        private void Awake()
        {
            // WebGLInput.captureAllKeyboardInput = true; 
            _lockerPanel = FindObjectOfType<CursorLockerPanel>();
            
            _pointerLockHook = FindObjectOfType<PointerLockHook>();
            var stick = FindObjectOfType<StickPointer>();
            _movementInput = new MovementInputSource(stick);
            var touchPointer = FindObjectOfType<TouchPointer>();
            _rotationInput = new RotationInputSource(touchPointer);
            var zoomTouch = FindObjectOfType<ZoomTouch>();
            _zoomInput = new ZoomInputSource(zoomTouch);
        }

        private void OnEnable()
        {
            _lockerPanel.PointerDowned += OnPointerDowned;
            _pointerLockHook.PointerLocked += OnPointerLocked;
            _pointerLockHook.PointerUnlocked += OnPointerUnlocked;
            _movementInput.Subscribe();
            _rotationInput.Subscribe();
            _zoomInput.Subscribe();
        }

        private void OnDisable()
        {
            _lockerPanel.PointerDowned -= OnPointerDowned;
            _pointerLockHook.PointerLocked -= OnPointerLocked;
            _pointerLockHook.PointerUnlocked -= OnPointerUnlocked;
            _movementInput.Unsubscribe();
            _rotationInput.Unsubscribe();
            _zoomInput.Unsubscribe();
        }

        private void OnPointerDowned()
        {
            if (_isPause)
                return;
            
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel") || Cursor.lockState == CursorLockMode.Locked
                && (Input.GetButtonDown("CursorUnlock")
                    || Input.GetButton("CursorUnlock")
                    || Input.GetButtonUp("CursorUnlock")))
            {
                //OnPointerUnlocked();
            }
            
            if (_isPause || _isUnlocked)
            {
                MouseInput = Vector2.zero;
                MovementInput = Vector2.zero;
                ScrollInput = 0f;
            }
            else
            {
                ScrollInput = _zoomInput.GetInput();
                if (Mathf.Approximately(ScrollInput, 0f))
                {
                    MouseInput = _rotationInput.GetInput();
                }
                else
                {
                    MouseInput = Vector3.zero;
                }
                MovementInput = _movementInput.GetInput();
            }
            _zoomInput.Reset();
            _rotationInput.Reset();
        }

        private void OnPointerLocked()
        {
            _isUnlocked = false;
        }

        private void OnPointerUnlocked()
        {
            _isUnlocked = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Pause()
        {
            _movementInput.Reset();
            _rotationInput.Reset();
            _zoomInput.Reset();
            OnPointerUnlocked();
            _isPause = true;
        }
    }
}