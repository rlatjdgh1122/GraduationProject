using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class SynergyBuildingDeadController : BuildingDeadController<SynergyBuilding>
{
    public UnityEvent OnDeadEvent = null;
    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += BuildingCompoOff;
        SignalHub.OnBattlePhaseEndEvent += BuildingCompoOn;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= BuildingCompoOff;
        SignalHub.OnBattlePhaseEndEvent -= BuildingCompoOn;
    }

    public override void OnDied()
    {
        base.OnDied();

        OnDeadEvent?.Invoke();
    }

    public void FixBuilding()
    {
        _owner.HealthCompo.ResetHealth();

        SetBuildingCondition(false);

        OnBuildingRepairEvent?.Invoke();
    }    
}