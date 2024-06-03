using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    [RequireComponent(typeof(PointerHandler))]
    public abstract class ButtonPointer : MonoBehaviour,
        ITouchButton
    {
        private PointerHandler _pointerHandler;

        public event UnityAction Downed;
        public event UnityAction Outed;

        public bool IsTouch => _pointerHandler.IsTouch;

        private void Awake()
        {
            _pointerHandler = GetComponent<PointerHandler>();
        }

        private void OnEnable()
        {
            _pointerHandler.PointerOuted += OnPointerOuted;
            _pointerHandler.PointerDowned += OnPointerDowned;
        }

        private void OnDisable()
        {
            _pointerHandler.PointerOuted -= OnPointerOuted;
            _pointerHandler.PointerDowned -= OnPointerDowned;
        }

        private void OnPointerOuted()
        {
            Outed?.Invoke();
        }

        private void OnPointerDowned(Vector2 position)
        {
            Downed?.Invoke();
        }
    }
}