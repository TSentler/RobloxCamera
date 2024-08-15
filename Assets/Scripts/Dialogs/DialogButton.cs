using UnityEngine;
using UnityEngine.Events;

namespace Dialogs
{
    public class DialogButton : MonoBehaviour
    {
        public event UnityAction OnClick; 

        public void Click()
        {
            OnClick?.Invoke();
        }
    }
}