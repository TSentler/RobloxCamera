using Mobile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseButtonSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _mobileUseButton;
    [SerializeField] private GameObject _desktopUseButton;

    private InputSetter _inputSetter;

    private void Awake()
    {
        _inputSetter = FindObjectOfType<InputSetter>();

        
        Hide();
    }

    public void Show()
    {
        if (_inputSetter.IsMobile)
        {
            _mobileUseButton.SetActive(true);
        }
        else
        {
            _desktopUseButton.SetActive(true);
        }
    }

    public void Hide()
    {
        _mobileUseButton.SetActive(false);
        _desktopUseButton.SetActive(false);
    }


}
