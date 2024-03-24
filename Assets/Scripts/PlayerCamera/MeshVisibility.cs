using UnityEngine;

namespace PlayerCamera
{
    [RequireComponent(typeof(ObstaclesAvoider))]
    public class MeshVisibility : MonoBehaviour
    {
        private readonly float _threshold = 0.01f;
        
        [Min(0f), SerializeField] private float _minRootDistance = 0.5f;
        [SerializeField] private Renderer[] _meshRenderers;

        private ObstaclesAvoider _avoider;
        private bool _isShowed = true;

        private void Awake()
        {
            _avoider = GetComponent<ObstaclesAvoider>();
        }

        private void OnEnable()
        {
            _avoider.Moved += OnMoved;
        }

        private void OnDisable()
        {
            _avoider.Moved -= OnMoved;
        }

        private void OnMoved()
        {
            if (_isShowed && 
                Vector3.Distance(transform.position, _avoider.CameraRoot.position) <
                _minRootDistance - _threshold)
            {
                Hide();
            }

            if (_isShowed == false &&
                Vector3.Distance(transform.position, _avoider.CameraRoot.position) >
                _minRootDistance + _threshold)
            {
                Show();
            }
        }

        private void Show()
        {
            _isShowed = true;
            Switch(_isShowed);
        }

        private void Hide()
        {
            _isShowed = false;
            Switch(_isShowed);
        }

        private void Switch(bool enabled)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].enabled = enabled;
            }
        }
    }
}
