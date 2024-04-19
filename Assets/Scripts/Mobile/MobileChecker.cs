#if UNITY_WEBGL && !UNITY_EDITOR
using Plugins.MobileIdentify;
#endif
using SocialNetwork;
using UnityEngine;

namespace Mobile
{
    public static class MobileChecker
    {
        public static bool IsMobile()
        {
            bool isMobile = Defines.IsVkMobileGames
#if UNITY_WEBGL && !UNITY_EDITOR
                            || MobileIdentificator.IsMobile()
#endif
                            || Application.isMobilePlatform
                            || Application.platform == RuntimePlatform.Android
                            || SystemInfo.deviceModel.StartsWith("iPad");

            return isMobile;
        }

        public static bool IsIOS()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return MobileIdentificator.IsIOSDevice();
#else
            return false;
#endif
        }
    }
}