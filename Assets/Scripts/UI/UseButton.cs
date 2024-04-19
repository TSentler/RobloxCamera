using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UseButton : MonoBehaviour, IPointerDownHandler
{
    public UnityAction Down;
        
    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Down?.Invoke();
    }
}
