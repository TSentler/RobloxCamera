using UnityEngine;
using UnityEngine.UI;

namespace UI.Joystick
{
    public class FloatingStick : MonoBehaviour
    {
        [SerializeField] private float Alpha = 0.5f;
        [SerializeField] private StickPointer _stickPointer;
        [SerializeField] private RectTransform _stickArea, _stickKnob;
        [SerializeField] private Image _areaImage, _knobImage;
        
        private void OnValidate()
        {
            if (_stickPointer == null)
                Debug.LogWarning("StickPointer was not found!", this);
            if (_stickArea == null)
                Debug.LogWarning("Stick area was not found!", this);
            if (_stickKnob == null)
                Debug.LogWarning("Stick knob was not found!", this);
        }

        
        private void OnEnable()
        {
            _stickPointer.Downed += OnFingerDowned;
            _stickPointer.Outed += OnFingerOuted;
            _stickPointer.Moved += MoveStickKnob;
        }

        private void OnDisable()
        {
            _stickPointer.Downed -= OnFingerDowned;
            _stickPointer.Outed -= OnFingerOuted;
            _stickPointer.Moved -= MoveStickKnob;
        }

        private void Start()
        {
            OnFingerOuted();
        }

        private void OnFingerDowned(Vector2 position)
        {
            _stickArea.position = new Vector3(position.x, position.y,
                _stickArea.position.z);
            Fade(0.25f);
        }

        private void OnFingerOuted()
        {
            Fade(Alpha);
            _stickKnob.anchoredPosition = Vector2.zero;
            _stickArea.anchoredPosition = Vector2.zero;
        }
        
        private void Fade(float alpha)
        {
            ChangeAlpha(ref _areaImage, alpha);
            ChangeAlpha(ref _knobImage, alpha);
        }

        private void ChangeAlpha(ref Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        private void MoveStickKnob(Vector2 position)
        {
            var radius = _stickArea.rect.height / 2;
            position *= radius;
            _stickKnob.localPosition = position;
        }
    }
}
