using UnityEngine;
using UnityEngine.Events;

namespace PlayerCamera
{
    [RequireComponent(typeof(MaxDistance))]
    public class ObstaclesAvoider : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Transform _cameraRoot;
        [Min(0f), SerializeField] private float _radius = 0.5f, _speed = 20f;

        private Vector3 _defaultMaxLocalPosition;
        private Collider[] _hitBuffer = new Collider[1];
        private float _overlapSpeedFactor = 3f;
        private MaxDistance _maxDistanceFactor;

        public event UnityAction Moved;
                
        public Transform CameraRoot => _cameraRoot;

        private Vector3 MaxLocalPosition => _defaultMaxLocalPosition * _maxDistanceFactor;
        
        private void Awake()
        {
            _maxDistanceFactor = GetComponent<MaxDistance>();
            _defaultMaxLocalPosition = transform.localPosition;
        }

        private void Update()
        {
            var targetLocalPosition = GetCameraLocalPosition();

            Move(targetLocalPosition);
        }

        private void Move(Vector3 targetLocalPosition)
        {
            if (IsArrived(targetLocalPosition))
                return;

            var step = _speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, targetLocalPosition, step);

            while (Physics.OverlapSphereNonAlloc(transform.position, _radius,
                       _hitBuffer, ~_playerLayer,
                       QueryTriggerInteraction.Ignore) > 0
                   && IsArrived(targetLocalPosition) == false)
            {
                transform.localPosition = Vector3.MoveTowards(
                    transform.localPosition, targetLocalPosition, 
                    step * _overlapSpeedFactor);
            }
            
            Moved?.Invoke();
        }

        private void SimpleMove(Vector3 targetLocalPosition)
        {
            if (IsArrived(targetLocalPosition))
                return;
            
            var step = _speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition, targetLocalPosition, step);
        }

        private bool IsArrived(Vector3 targetLocalPosition) =>
            (targetLocalPosition - transform.localPosition).magnitude < 0.001f;

        private Vector3 GetCameraLocalPosition()
        {
            Vector3 cameraLocalPosition;
            Vector3 maxCameraPosition =
                _cameraRoot.TransformPoint(MaxLocalPosition);
            Collider[] hits = Physics.OverlapCapsule(_cameraRoot.position,
                maxCameraPosition, _radius, ~_playerLayer,
                QueryTriggerInteraction.Ignore);
            if (hits.Length > 0)
            {
                Vector3 avoidVector =
                    GetAvoidedVector(_cameraRoot.position, maxCameraPosition,
                        _radius);
                cameraLocalPosition =
                    _cameraRoot.InverseTransformPoint(_cameraRoot.position + avoidVector);
            }
            else
            {
                cameraLocalPosition = MaxLocalPosition;
            }

            return cameraLocalPosition;
        }

        private Vector3 GetAvoidedVector(Vector3 start, Vector3 end, 
            float radius)
        {
            Vector3 startOffset = (start - end).normalized 
                         * radius / 2f;
            Vector3 direction = end - (start + startOffset);
            Ray ray = new Ray(start + startOffset, direction.normalized);
            if (Physics.SphereCast(ray, radius, out var hit,
                    direction.magnitude, ~_playerLayer,
                    QueryTriggerInteraction.Ignore))
            {
                Vector3 startToHitVector = hit.point - start;
                Vector3 startToObstacleVector =
                    Vector3.Project(startToHitVector, direction.normalized);
                return startToObstacleVector;
            }
            
            return Vector3.zero;
        }
    }
}
