using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _actions;

    private void Awake()
    {
        _actions = new PlayerInputActions();
        _actions.Player.Enable();
    }

    public Vector2 GetNormalizedMovementDirection()
    {
        return _actions.Player.Move.ReadValue<Vector2>().normalized;
    }
}