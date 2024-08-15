using Player;
using UnityEngine;

namespace Dialogs
{
    public class DialogActivator : MonoBehaviour, IMouseActivator
    {
        public GameObject Dialog;

        private MouseStateHandler _mouseStateHandler;

        private void Awake()
        {
            _mouseStateHandler = FindObjectOfType<MouseStateHandler>();
            Dialog.SetActive(false);
        }

        public void Activate()
        {
            Dialog.SetActive(true); //Активация геймобъекта с канвасом диалогов
            _mouseStateHandler.EnableMouse(this);
        }

        public void Deactivate()
        {
            Dialog.SetActive(false);
            _mouseStateHandler.DisableMouse(this);
        }
    }
}