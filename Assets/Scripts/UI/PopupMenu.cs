using Player;
using UnityEngine;

namespace UI
{
    public class PopupMenu : MonoBehaviour, IMouseActivator
    {
        [SerializeField] private bool _isOpenByStart;
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
            if (_isOpenByStart)
            {
                Open();
            }
            else
            {
                _root.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Menu"))
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
            _mouseStateHandler.EnableMouse(this);
        }

        public void Close()
        {
            _mouseStateHandler.DisableMouse(this);
            _root.SetActive(false);
        }
    }
}
