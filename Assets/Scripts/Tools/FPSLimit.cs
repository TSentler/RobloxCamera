using UnityEngine;

namespace Tools
{
    public class FPSLimit : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 30;

        private void OnEnable()
        {
            Application.targetFrameRate = _targetFrameRate; 
        }

        private void Update()
        {
            // Application.targetFrameRate = _targetFrameRate; 
        }
    }
}
