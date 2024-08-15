using Player;
using UnityEngine;

namespace Dialogs
{
    public class DialogActivator : MonoBehaviour, IMouseActivator
    {
        public GameObject Root;

        private MouseStateHandler _mouseStateHandler;

        private void Awake()
        {
            _mouseStateHandler = FindObjectOfType<MouseStateHandler>();
            Root.SetActive(false);
        }

        public void Activate()
        {
            Root.SetActive(true); //Активация геймобъекта с канвасом диалогов
            _mouseStateHandler.EnableMouse(this);
        }

        public void Deactivate()
        {
            Root.SetActive(false);
            _mouseStateHandler.DisableMouse(this);
        }
    }
}