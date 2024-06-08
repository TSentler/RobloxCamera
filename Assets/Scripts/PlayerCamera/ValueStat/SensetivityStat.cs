using UnityEngine;
using ValueStat;

namespace PlayerCamera.ValueStat
{
    public class SensetivityStat : MonoBehaviour, IFloatStatWriter
    {
        [SerializeField] private float _value = 1f;

        public float Value => _value;

        public void WriteValue(float value)
        {
            _value = value;
        }
    }
}
