using UnityEngine;

namespace ValueStat
{
    public interface IFloatStat
    {
        public float Value { get; }
    }

    public interface IFloatStatWriter : IFloatStat
    {
        public void WriteValue(float value);
    }

}
