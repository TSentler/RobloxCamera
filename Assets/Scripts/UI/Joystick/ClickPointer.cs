using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class ClickPointer : MonoBehaviour, IPointerDownHandler
{
    public event UnityAction Downed;

    public void OnPointerDown(PointerEventData eventData)
    {
        Downed?.Invoke();
    }
}