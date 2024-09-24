using GamePause;
using MobileInput;
using UnityEngine;

namespace Player
{
    public class DesktopMouseLocker : MonoBehaviour, IMouseActivator
    {
        private PauseHandler _pauseHandler;
        private CursorLockerPanel _lockerPanel;
        private MouseStateHandler _mouseStateHandler;

        private void Awake()
        {
            _lockerPanel = FindObjectOfType<CursorLockerPanel>();
            _pauseHandler = FindObjectOfType<PauseHandler>();
            _mouseStateHandler = FindObjectOfType<MouseStateHandler>();
        }

        private void OnEnable()
        {
            _lockerPanel.PointerDowned += OnPointerDowned;
        }

        private void OnDisable()
        {
            _lockerPanel.PointerDowned -= OnPointerDowned;
        }
        
        private void OnPointerDowned()
        {
            if (_pauseHandler.IsPause)
                return;
            
            _mouseStateHandler.DisableMouse(this);
        }
        
    }
}