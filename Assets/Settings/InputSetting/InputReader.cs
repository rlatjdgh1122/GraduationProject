using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPenguinActions, Controls.IBuildingActions
{
    public event Action RightClickEvent;
    public event Action OnLeftClickEvent;

    public event Action OnEscEvent;

    public event Action OnEBtnEvent;
    public event Action OnQBtnEvent;

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
            OnEscEvent?.Invoke();
        }
    }

    public void OnRotateRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnEBtnEvent?.Invoke();
        }
    }

    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnQBtnEvent?.Invoke();
        }
    }

   public bool IsPointerOverUI()
    {
        // 마우스 포인터가 UI 위에 있는지 확인
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        Debug.Log(results.Count);
        return results.Count > 0;
    }
}
