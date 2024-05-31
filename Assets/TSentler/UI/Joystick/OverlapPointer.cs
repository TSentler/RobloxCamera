using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class OverlapPointer : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform _overlapTarget;

        public event UnityAction Downed;

        private void OnValidate()
        {
            if (_overlapTarget == null)
                Debug.LogWarning("_overlapTarget was not found!", this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OverlapTarget(eventData.position))
            {
                Downed?.Invoke();
            }
        }

        private bool OverlapTarget(Vector2 position)
        {
            var res = RectTransformUtility
                .RectangleContainsScreenPoint(_overlapTarget, position);
            return res;
        }
    }
}