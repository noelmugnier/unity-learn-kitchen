using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _actions;

    public event EventHandler OnInteractAction;

    private void Awake()
    {
        _actions = new PlayerInputActions();
        _actions.Player.Enable();

        _actions.Player.Interact.performed += InteractPerformed;
    }

    private void OnDestroy()
    {
        _actions.Player.Interact.performed -= InteractPerformed;
    }

    private void InteractPerformed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);            
    }

    public Vector2 GetNormalizedMovementDirection()
    {
        return _actions.Player.Move.ReadValue<Vector2>().normalized;
    }
}