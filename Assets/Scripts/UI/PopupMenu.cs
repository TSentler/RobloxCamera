using Player;
using UnityEngine;

namespace UI
{
    public class PopupMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _root;

        private MouseStateHandler _mouseStateHandler;
        private InputSetter _inputSetter;

        private void Awake()
        {
            _mouseStateHandler = FindObjectOfType<MouseStateHandler>();
            _inputSetter = FindObjectOfType<InputSetter>();
        }

        private void Start()
        {
            Open();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                Open();
            }

            if (_inputSetter.IsMobile == false && _mouseStateHandler.IsMouseEnable == false)
            {
                Close();
            }
        }

        public void Open()
        {
            _root.SetActive(true);
            _mouseStateHandler.EnableMouse();
        }

        public void Close()
        {
            _mouseStateHandler.DisableMouse();
            _root.SetActive(false);
        }
    }
}
