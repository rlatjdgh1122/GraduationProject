using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveEvent : MonoBehaviour
{
    public UnityEvent OnBattleStartEvent = null;
    public UnityEvent OnBattleEndEvent = null;

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += OnStart;
        SignalHub.OnBattlePhaseEndEvent += OnEnd;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnStart;
        SignalHub.OnBattlePhaseEndEvent -= OnEnd;
    }

    private void OnStart()
    {
        OnBattleStartEvent?.Invoke();
    }

    private void OnEnd()
    {
        OnBattleEndEvent?.Invoke();
    }


}
