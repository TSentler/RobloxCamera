using UnityEngine;
using UnityEngine.Events;
using Interfaces;

namespace Interactions
{
    public class InteractHandler : MonoBehaviour, IActivatable
    {
        private bool _isEnable = true;

        public event UnityAction Interacted;

        public void Activate()
        {
            _isEnable = true;
        }

        public void Deactivate()
        {
            _isEnable = false;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Interact"))
            {
                Interact();
            }
        }

        public void Interact()
        {
            if (_isEnable == false)
                return;

            Interacted?.Invoke();
        }
    }
}