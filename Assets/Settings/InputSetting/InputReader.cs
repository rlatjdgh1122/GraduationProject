using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Controls.IPenguinActions, Controls.IBuildingActions
{
    public event Action OnBattleRightClickEvent;
    public event Action OnBattleLeftClickEvent;
    public event Action OnLeftClickEvent;

    public event Action OnEscEvent;

    public event Action OnEBtnEvent;
    public event Action OnQBtnEvent;

    public event Action OnSkillEvent;
    public event Action OnUltimateEvent;

    private Controls _controls;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Penguin.SetCallbacks(this);
            _controls.Building.SetCallbacks(this);
        }
        _controls.Penguin.Disable();
        _controls.Building.Enable();

        SignalHub.OnBattlePhaseStartEvent += BattleStart;
        SignalHub.OnBattlePhaseEndEvent += BattleEnd;
    }

    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= BattleStart;
        SignalHub.OnBattlePhaseEndEvent -= BattleEnd;
    }

    private void BattleStart()
    {
        _controls.Penguin.Enable();
        _controls.Building.Disable();
    }

    private void BattleEnd()
    {
        _controls.Penguin.Disable();
        _controls.Building.Enable();
    }

    public void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed /*&& IsPointerOverUI()*/)
        {
            OnLeftClickEvent?.Invoke();
        }
    }

    public void OnExitBuilding(InputAction.CallbackContext context)
    {
        if (context.performed)
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


        //foreach (var ray in results)
        //{
        //    Debug.Log(ray.gameObject.name);
        //}

        return results.Count > 0;
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSkillEvent?.Invoke();
        }
    }

    public void OnUltimate(InputAction.CallbackContext context)
    {
        OnUltimateEvent?.Invoke();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnBattleRightClickEvent?.Invoke();
        }
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnBattleLeftClickEvent?.Invoke();
        }
    }
}
