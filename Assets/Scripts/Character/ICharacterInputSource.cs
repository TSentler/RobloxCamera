using UnityEngine;

namespace Character
{
    public interface ICharacterInputSource
    {
        bool AttackInputDown { get; }
        bool AttackInput { get; }
        Vector2 MovementInput { get; }
        Vector2 MouseInput { get; }
        bool IsJumpInputDown { get; }
        bool IsJumpInput { get; }
        bool IsShiftInput { get; }
    }
}
