using System;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogs
{
    public class Dialog : MonoBehaviour
    {
        public bool ActicateByStart;
        public UnityEvent OnStarted;
        public UnityEvent OnEnded;

        private Phrase _phrase; //ссылка на компонент текущей фразы
        private DialogActivator _dialogActivator;
        private DialogView _dialogView;
        private DialogButton _dialogButton;
        private Camera _currentCamera;
        private bool _isCurrent;
        private bool _isClicked;

        private void Awake()
        {
            _phrase = GetComponent<Phrase>(); //с помощью метода GetComponent находим первый рядом лежащий компонент 
            _dialogView = FindObjectOfType<DialogView>(); //ищем компонент DialogView который гдето на сцене(на одном из гейм объектв).
            _dialogActivator = FindObjectOfType<DialogActivator>(); //изначально любая ссылка например _dialogActivator пустая как если бы ей был присвоен null _dialogActivator = null
            _dialogButton = FindObjectOfType<DialogButton>();
        }

        private void OnEnable()
        {
            _dialogButton.OnClick += OnDialogClick;
        }

        private void OnDisable()
        {
            _dialogButton.OnClick -= OnDialogClick;
        }

        private void Start()
        {
            if (ActicateByStart)
            {
                StartDialog();
            }
        }

        public void StartDialog()
        {
            Debug.Log(gameObject.name);
            _isCurrent = true;
            _dialogActivator.Activate();
            Say();
            OnStarted.Invoke();
        }

        public void NextPhrase()
        {
            if (_isCurrent == false)
                return;

            Say();
        }

        private void Say()
        {
            if (_phrase != null)
            {
                _dialogView.SetPhrase(_phrase);
                CameraActivate();
                _phrase = _phrase.NextPhrase;
            }
            else
            {
                _isCurrent = false;
                _dialogActivator.Deactivate();
                CameraDeactivate();
                OnEnded.Invoke();
                _phrase = GetComponent<Phrase>();
            }
        }

        private void CameraActivate()
        {
            if (_phrase.Camera == null)
                return;

            CameraDeactivate();
            _currentCamera = _phrase.Camera;
            _currentCamera.gameObject.SetActive(true);
        }

        private void CameraDeactivate()
        {
            if (_currentCamera != null)
            {
                _currentCamera.gameObject.SetActive(false);
            }
        }

        private void OnDialogClick()
        {
            _isClicked = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || _isClicked)
            {
                NextPhrase();
            }
            _isClicked = false;
        }
    }
}