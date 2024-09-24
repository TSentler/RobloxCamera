using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Interactions
{
    public class UseButton : MonoBehaviour, IPointerDownHandler
    {
        private InteractHandler _interactHandler;

        private void Awake()
        {
            _interactHandler = FindObjectOfType<InteractHandler>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _interactHandler.Interact();
        }
    }
}