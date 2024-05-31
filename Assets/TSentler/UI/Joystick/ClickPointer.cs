using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public abstract class ClickPointer : MonoBehaviour, IPointerDownHandler
    {
        public event UnityAction Downed;

        public void OnPointerDown(PointerEventData eventData)
        {
            Downed?.Invoke();
        }
    }
}