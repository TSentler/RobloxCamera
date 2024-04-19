using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MobileInput
{
    public class CursorLockerPanel : MonoBehaviour, IPointerDownHandler
    {
        public event UnityAction PointerDowned;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDowned?.Invoke();
        }
    }
}
