using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SynergyBuildingDeadController : BuildingDeadController<SynergyBuilding>
{
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
    }

    public void FixBuilding()
    {
        _owner.HealthCompo.ResetHealth();

        SetBuildingCondition(false);
    }    
}