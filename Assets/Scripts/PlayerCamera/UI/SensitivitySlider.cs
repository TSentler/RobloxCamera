using ValueStat;
using PlayerCamera.ValueStat;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCamera.UI
{
    [RequireComponent(typeof(Slider))]
    public class SensitivitySlider : MonoBehaviour
    {
        private Slider _slider;
        private IFloatStatWriter _sensetivity;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _sensetivity = FindObjectOfType<SensetivityStat>();
        }

        private void OnEnable()
        {
            _slider.SetValueWithoutNotify(_sensetivity.Value);
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _sensetivity.WriteValue(value);
        }
    }
}
