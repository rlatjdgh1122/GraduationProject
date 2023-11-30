using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPenguinActions
{
    public event Action ClickEvent;

    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Penguin.SetCallbacks(this);
        }

        _controls.Penguin.Enable();
    }

    public void OnMouseRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ClickEvent?.Invoke();
        }
    }
}
