using GamePause;
using MobileInput;
using PlayerCamera;
using UI.Joystick;
using UnityEngine;
using ICharacterInputSource = Character.ICharacterInputSource;

namespace Player.InputSource
{
    public class PlayerInput : MonoBehaviour, ICharacterInputSource, IPausable, IScrollInputSource
    {
        private CursorLockerPanel _lockerPanel;
        private MovementInputSource _movementInput;
        private RotationInputSource _rotationInput;
        private MouseStateHandler _mouseStateHandler;
        private AttackInputSource _attackInput;
        private JumpInputSource _jumpInput;
        private InputSetter _inputSetter;
        private bool _isRunLock;
        private PauseHandler _pauseHandler;

        public bool AttackInputDown { get; private set; }
        public bool AttackInput { get; private set; }
        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseInput { get; private set; }
        public bool IsJumpInput { get; private set; }
        public bool IsShiftInput { get; private set; }
        public float ScrollInput { get; private set; }

        private void Awake()
        {
            // WebGLInput.captureAllKeyboardInput = true; 
            _lockerPanel = FindObjectOfType<CursorLockerPanel>();
            var stick = FindObjectOfType<StickPointer>();
            _movementInput = new MovementInputSource(stick);
            var touchPointer = FindObjectOfType<TouchPointer>();
            _rotationInput = new RotationInputSource(touchPointer);
            OverlapPointer attackOverlapPointer = FindObjectOfType<ShootOverlapPointer>();
            _inputSetter = FindObjectOfType<InputSetter>();
            _attackInput = new AttackInputSource(touchPointer, attackOverlapPointer, _inputSetter);
            var jumpPointer = FindObjectOfType<JumpClickPointer>();
            _jumpInput = new JumpInputSource(jumpPointer);
            
            _mouseStateHandler = FindObjectOfType<MouseStateHandler>();
            _pauseHandler = FindObjectOfType<PauseHandler>();
            if (_pauseHandler.IsPause)
            {
                Pause();
            }
            _pauseHandler.Subscribe(this);
            //_agent.updateRotation = false;
        }

        private void OnDestroy()
        {
            _pauseHandler.UnSubscribe(this);
        }

        private void OnEnable()
        {
            _lockerPanel.PointerDowned += OnPointerDowned;
            _movementInput.Subscribe();
            _rotationInput.Subscribe();
            _attackInput.Subscribe();
            _jumpInput.Subscribe();
        }

        private void OnDisable()
        {
            _lockerPanel.PointerDowned -= OnPointerDowned;
            _movementInput.Unsubscribe();
            _rotationInput.Unsubscribe();
            _attackInput.Unsubscribe();
            _jumpInput.Unsubscribe();
        }

        private void Update()
        {
            if (_pauseHandler.IsPause
                || _mouseStateHandler.IsMouseEnable)
            {
                DefaultInput();
            }
            else
            {
                ReadInput();
            }

            _rotationInput.Reset();
        }

        private void DefaultInput()
        {
            AttackInputDown = AttackInput = IsJumpInput = IsShiftInput = false;
            MovementInput = MouseInput = Vector2.zero;
            ScrollInput = 0f;
        }

        private void ReadInput()
        {
            AttackInputDown = _attackInput.GetInputDown();
            AttackInput = _attackInput.GetInput();
            IsJumpInput = _jumpInput.GetInputDown();
            IsShiftInput = _inputSetter.IsMobile 
                           || Input.GetKey(KeyCode.LeftShift) || _isRunLock;
            
            MouseInput = _rotationInput.GetInput();
            MovementInput = _movementInput.GetInput();
            ScrollInput = Input.mouseScrollDelta.y;


            Debug.Log("IsJumpInput " + IsJumpInput);
            Debug.Log("IsShiftInput " + IsShiftInput);
            Debug.Log("AttackInputDown " + AttackInputDown);
            Debug.Log("AttackInput " + AttackInput);
            Debug.Log("ScrollInput " + ScrollInput);
        }

        public void Pause()
        {
            _movementInput?.Reset();
            _rotationInput?.Reset();
            _mouseStateHandler?.EnableMouse();
        }

        public void Unpause()
        {
        }
        
        public void Run()
        {
            _isRunLock = !_isRunLock;
        }
        
        private void OnPointerDowned()
        {
            if (_pauseHandler.IsPause)
                return;
            
            _mouseStateHandler?.DisableMouse();
        }
    }
}
