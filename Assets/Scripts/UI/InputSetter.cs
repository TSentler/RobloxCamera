using Mobile;
using SocialNetwork;
using System.Collections.Generic;
using UnityEngine;

public class InputSetter : MonoBehaviour, IMobilable
{
    [SerializeField] private bool _isMobileEmul;
    [SerializeField] private List<GameObject> _mobileUI;
    [SerializeField] private List<GameObject> _desktopUI;

    private bool _isMobile;

    public bool IsMobile => _isMobile;

    private void Awake()
    {
        _isMobile = false;
        if (MobileChecker.IsMobile()
            || _isMobileEmul && Defines.IsUnityEditor)
        {

            _isMobile = true;
        }
    }

    private void Start()
    {
        _desktopUI.ForEach(item => item.SetActive(false));
        _mobileUI.ForEach(item => item.SetActive(false));

        ActivateDevicePanels(_isMobile);
    }

    private void ActivateDevicePanels(bool isMobile)
    {
        if (isMobile)
        {
            _mobileUI.ForEach(item => item.SetActive(true));
        }
        else
        {
            _desktopUI.ForEach(item => item.SetActive(true));
        }
    }
}
