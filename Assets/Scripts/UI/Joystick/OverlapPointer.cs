using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OverlapPointer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private RectTransform _overlapTarget;

    public event UnityAction Downed;

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
