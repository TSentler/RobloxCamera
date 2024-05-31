using UI.Joystick;
using UnityEngine;

namespace UI.Joystick
{
    public class StickyButton : MonoBehaviour
    {
        [SerializeField] private OverlapPointer _overlapPointer;
        [SerializeField] private PointerHandler _pointerHandler;
        [SerializeField] private RectTransform _stickArea;

        private bool _isOverlapped;

        private void OnValidate()
        {
            if (_overlapPointer == null)
                Debug.LogWarning("_overlapPointer was not found!", this);
            if (_pointerHandler == null)
                Debug.LogWarning("_pointerHandler was not found!", this);
            if (_stickArea == null)
                Debug.LogWarning("Stick area was not found!", this);
        }

        private void OnEnable()
        {
            _overlapPointer.Downed += OnDowned;
            _pointerHandler.PointerMoved += OnMoved;
            _pointerHandler.PointerOuted += OnOuted;
        }

        private void OnDisable()
        {
            _overlapPointer.Downed -= OnDowned;
            _pointerHandler.PointerMoved -= OnMoved;
            _pointerHandler.PointerOuted -= OnOuted;
            OnOuted();
        }

        private void Start()
        {
            OnOuted();
        }

        private void OnOuted()
        {
            _isOverlapped = false;
            _stickArea.anchoredPosition = Vector2.zero;
        }

        private void OnMoved(Vector2 position)
        {
            if (_isOverlapped == false)
                return;

            _stickArea.position = new Vector3(position.x, position.y,
                _stickArea.position.z);
        }

        private void OnDowned()
        {
            _isOverlapped = true;
        }
    }
}