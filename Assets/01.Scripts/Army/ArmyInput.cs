using ArmySystem;
using Define.RayCast;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmyInput : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public UnityEvent OnRightClickEvent = null;
    public UnityEvent OnLeftClickEvent = null;
    public UnityEvent<RaycastHit> OnRightClickRaycastHitEvent = null;
    public UnityEvent<RaycastHit> OnLeftClickRaycastHitEvent = null;

    public UnityEvent<int> OnClickNumberKeyEvent = null;

    private Dictionary<KeyCode, Action> keyDictionary = new();

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += SubscribeToBattlePhaseEvents;
        SignalHub.OnBattlePhaseEndEvent += UnSubscribeToBattlePhaseEvents;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= SubscribeToBattlePhaseEvents;
        SignalHub.OnBattlePhaseEndEvent -= UnSubscribeToBattlePhaseEvents;
    }

    private void SubscribeToBattlePhaseEvents() // 전투시작하면 ArmyInput 구독
    {
        _inputReader.RightClickEvent += OnRightClickEventHandler;
        _inputReader.OnLeftClickEvent += OnLeftClickEventHandler;
    }

    private void UnSubscribeToBattlePhaseEvents() // 전투끝나면 ArmyInput 구독 해제
    {
        _inputReader.RightClickEvent -= OnRightClickEventHandler;
        _inputReader.OnLeftClickEvent -= OnLeftClickEventHandler;
    }

    private void Awake()
    {
        keyDictionary = new Dictionary<KeyCode, Action>()
        {
             {KeyCode.Alpha1, ()=> OnClickNumberKeyEventHandler(1) },
             {KeyCode.Alpha2, ()=> OnClickNumberKeyEventHandler(2) },
             {KeyCode.Alpha3, ()=> OnClickNumberKeyEventHandler(3) },
             {KeyCode.Alpha4, ()=> OnClickNumberKeyEventHandler(4) },
             {KeyCode.Alpha5, ()=> OnClickNumberKeyEventHandler(5) },
             {KeyCode.Alpha6, ()=> OnClickNumberKeyEventHandler(6) },
             {KeyCode.Alpha7, ()=> OnClickNumberKeyEventHandler(7) },
             {KeyCode.Alpha8, ()=> OnClickNumberKeyEventHandler(8) },
             {KeyCode.Alpha9, ()=> OnClickNumberKeyEventHandler(9) },
        };
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var dic in keyDictionary)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }//end if

    }

    public void OnRightClickEventHandler()
    {
        if (!ArmyManager.CheckInstance()) return;
        if (ArmyManager.Instance.CurArmy == null) return;
        if (ArmyManager.Instance.CurArmy.IsHealing) return;
        if (!WaveManager.Instance.IsBattlePhase) return;

        OnRightClickEvent?.Invoke();

        RaycastHit? hit = GetRaycasHit();
        if (hit != null)
            OnRightClickRaycastHitEvent?.Invoke(hit.Value);
    }

    public void OnLeftClickEventHandler()
    {
        if (!ArmyManager.CheckInstance()) return;
        if (ArmyManager.Instance.CurArmy == null) return;
        if (ArmyManager.Instance.CurArmy.IsHealing) return;
        if (!WaveManager.Instance.IsBattlePhase) return;

        OnLeftClickEvent?.Invoke();

        RaycastHit? hit = GetRaycasHit();
        if (hit != null)
            OnLeftClickRaycastHitEvent?.Invoke(hit.Value);
    }

    public void OnClickNumberKeyEventHandler(int keyNumber)
    {
        OnClickNumberKeyEvent?.Invoke(keyNumber);
    }


    private RaycastHit? GetRaycasHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(RayCasts.MousePointRay, out hit))
        {
            return hit;
        }
        return null;
    }
}
