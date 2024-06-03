using UnityEngine;
using UnityEngine.Events;

namespace UI.Joystick
{
    public interface ITouchable: ITouchHandler, ITouchOutHandler
    {
        public event UnityAction<Vector2> Downed, Moved;
    }

    public interface ITouchButton: ITouchHandler, ITouchDownHandler,
        ITouchOutHandler
    {

    }

    public interface ITouchDownHandler
    {
        public event UnityAction Downed;
    }

    public interface ITouchOutHandler
    {
        public event UnityAction Outed;
    }

    public interface ITouchHandler
    {
        public bool IsTouch { get; }
    }
}