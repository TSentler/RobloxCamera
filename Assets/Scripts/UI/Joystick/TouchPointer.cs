using UnityEngine;
using UnityEngine.Events;

namespace UI.Joystick
{
    [RequireComponent(typeof(PointerHandler))]
    public class TouchPointer : MonoBehaviour, ITouchable
    {
        private readonly float _minMove = 0.05f;
        private readonly float _maxMove = 120f;

        [Min(0.01f), SerializeField] private float _sensitivity = 20f;

        public void Set(float s)
        {
            _sensitivity = s;
        }
        private PointerHandler _pointerHandler;
        private Vector2 _previousTouch, _currentTouch;
        private float _maxMagnitude = 0f;
        
        public event UnityAction Outed;
        public event UnityAction<Vector2> Downed, Moved;

        public bool IsTouch => _pointerHandler.IsTouch;
        
        private void Awake()
        {
            _pointerHandler = GetComponent<PointerHandler>();
        }

        private void OnEnable()
        {
            _pointerHandler.PointerOuted += OnPointerOuted;
            _pointerHandler.PointerDowned += OnPointerDowned;
            _pointerHandler.PointerMoved += OnPointerMoved;
        }

        private void OnDisable()
        {
            _pointerHandler.PointerOuted -= OnPointerOuted;
            _pointerHandler.PointerDowned -= OnPointerDowned;
            _pointerHandler.PointerMoved -= OnPointerMoved;
        }

        private void OnPointerOuted()
        {
            Outed?.Invoke();
        }

        private void OnPointerDowned(Vector2 position)
        {
            _previousTouch = _currentTouch = position;
            Downed?.Invoke(_currentTouch);
        }

        private void OnPointerMoved(Vector2 position)
        {
            _previousTouch = _currentTouch;
            _currentTouch = position;
            var moveVector = CalculateMoveVector(_currentTouch, 
                _previousTouch);
            Moved?.Invoke(moveVector);
        }

        private Vector2 CalculateMoveVector(Vector2 currentPosition, 
            Vector2 previousPosition)
        {
            
            var move = currentPosition - previousPosition;
            // move = new Vector2(move.x / Screen.width, move.y / Screen.height);
            move *= _sensitivity / Screen.dpi;
            
            if (_maxMagnitude < move.magnitude)
            {
                Debug.Log(move.magnitude);
                _maxMagnitude = move.magnitude;
            }
            
            if (move.magnitude < _minMove)
                move = Vector2.zero;
            else if (move.magnitude > _maxMove) 
                move = Vector2.zero;
                // move = move.normalized * _maxMove;

            return move;
        }

    }
}
