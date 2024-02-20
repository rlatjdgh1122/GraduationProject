using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPenguinActions, Controls.IBuildingActions
{
    public event Action RightClickEvent;
    public event Action OnLeftClickEvent;

    public event Action OnExitInstallEvent;

    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Penguin.SetCallbacks(this);
            _controls.Building.SetCallbacks(this);
        }

        _controls.Penguin.Enable();
        _controls.Building.Enable();
    }

    public void OnMouseRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RightClickEvent?.Invoke();
        }
    }

    public void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if(context.performed /*&& IsPointerOverUI()*/)
        {
            OnLeftClickEvent?.Invoke();
        }
    }

    public void OnExitBuilding(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnExitInstallEvent?.Invoke();
        }
    }

    //public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();
}
