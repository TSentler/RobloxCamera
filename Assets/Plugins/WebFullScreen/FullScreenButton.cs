using UnityEngine;

namespace Plugins.WebFullScreen
{
    public class FullScreenButton : MonoBehaviour
    {
        public void SetFullScreen()
        {
#if WEBGL_DEFINE
            FullScreen.SetFullScreen();
#endif
        }

        public void ExitFullScreen()
        {
#if WEBGL_DEFINE
            FullScreen.ExitFullScreen();
#endif
        }
    }
}