using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class ETrigger : MonoBehaviour
    {
        public UnityEvent OnTriggered;

        private InteractHandler _interactHandler;

        private void Awake()
        {
            _interactHandler = FindObjectOfType<InteractHandler>();
        }

        private void OnDisable()
        {
            _interactHandler.Interacted -= OnTriggered.Invoke;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                _interactHandler.Interacted += OnTriggered.Invoke;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                _interactHandler.Interacted -= OnTriggered.Invoke;
            }
        }
    }
}