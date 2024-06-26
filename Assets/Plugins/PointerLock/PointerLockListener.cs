using System.Runtime.InteropServices;

namespace Plugins.PointerLock
{
    public static class PointerLockListener
    {
        [DllImport("__Internal")]
        public static extern void PointerLockListenerInitialize();
        
        [DllImport("__Internal")]
        public static extern void AddPointerLockListener();
        
        [DllImport("__Internal")]
        public static extern void RemovePointerLockListener();
    }
}