using UnityEngine;

namespace MobileInput
{
    public interface ICharacterInputSource
    {
        Vector2 MovementInput { get; }
        Vector2 MouseInput { get; }
        float ScrollInput { get; }
    }
}