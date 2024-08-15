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

        private Phrase _currentPhrase; //ссылка на компонент текущей фразы
        private Phrase _firstPhrase;
        private DialogActivator _dialogActivator;
        private DialogView _dialogView;
        private DialogButtons _dialogButton;
        private Camera _currentCamera;
        private bool _isCurrent;
        private bool _isClicked;

        private void Awake()
        {
            _firstPhrase = GetComponent<Phrase>(); //с помощью метода GetComponent находим первый рядом лежащий компонент 
            _dialogView = FindObjectOfType<DialogView>(); //ищем компонент DialogView который гдето на сцене(на одном из гейм объектв).
            _dialogActivator = FindObjectOfType<DialogActivator>(); //изначально любая ссылка например _dialogActivator пустая как если бы ей был присвоен null _dialogActivator = null
            _dialogButton = FindObjectOfType<DialogButtons>();
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
            _currentPhrase = _firstPhrase;
            Say();
            OnStarted.Invoke();
        }

        public void NextPhrase()
        {
            if (_isCurrent == false)
                return;

            _currentPhrase = _currentPhrase.GetNextPhrase();
            Say();
        }

        private void Say()
        {
            if (_currentPhrase != null)
            {
                _dialogView.SetPhrase(_currentPhrase);
                CameraActivate();
            }
            else
            {
                _isCurrent = false;
                _dialogActivator.Deactivate();
                CameraDeactivate();
                OnEnded.Invoke();
            }
        }

        private void CameraActivate()
        {
            if (_currentPhrase.Camera == null)
                return;

            CameraDeactivate();
            _currentCamera = _currentPhrase.Camera;
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
            NextPhrase();
        }
    }
}