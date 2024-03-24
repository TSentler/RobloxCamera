using PlayerCamera;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IScrollInputSource
{
    public float ScrollInput {  get; private set; }

    void Update()
    {
        ScrollInput = Input.mouseScrollDelta.y;
    }
}
