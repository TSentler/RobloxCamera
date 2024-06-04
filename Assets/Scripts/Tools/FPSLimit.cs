using UnityEngine;

namespace Tools
{
    public class FPSLimit : MonoBehaviour
    {
        [Header("At WebGl allwayse will be 0f")]
        [SerializeField] private int _targetFrameRate = 30;

        private void Awake()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            _targetFrameRate = 0f;
#endif
        }

        private void OnEnable()
        {
            Application.targetFrameRate = _targetFrameRate; 
        }

        private void Update()
        {
            Application.targetFrameRate = _targetFrameRate; 
        }
    }
}
