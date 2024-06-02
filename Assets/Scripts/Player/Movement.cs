using Character;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Movement : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;
        
        public event UnityAction<Vector2> Moved;

        protected ICharacterInputSource InputSource { get; private set; }
        protected Rigidbody Rigidbody { get; private set; }
        protected bool CanMove { get; private set; } = true;

        private void OnValidate()
        {
            if (_inputSourceBehaviour 
                && !(_inputSourceBehaviour is ICharacterInputSource))
            {
                Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
                _inputSourceBehaviour = null;
            }
        } 
        
        private void Initialize(ICharacterInputSource inputSource)
        {
            if (InputSource == null)
            {
                InputSource = inputSource;
            }    
        }
        
        protected void Awake()
        {
            Initialize((ICharacterInputSource)_inputSourceBehaviour);
            Rigidbody = GetComponent<Rigidbody>();
        }

        public virtual void SetDirection(Vector2 direction)
        {
            Moved?.Invoke(direction);
        }

        public void Stop()
        {
            CanMove = false;
            Rigidbody.isKinematic = true;
        }

        public void Go()
        {
            CanMove = true;
            Rigidbody.isKinematic = false;
        }
    }
}