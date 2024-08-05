using ArmySystem;
using SynergySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;


[RequireComponent(typeof(HealingSystem))]
public class SynergyBuildingMouseEventHandler : MouseEventHandler
{
    [SerializeField] private SynergyBuilding _owner = null;

    public SynergyType BuildingSynergyType => _owner.BuildingSynergyType;

    private HealingSystem _healingSystem = null;

    protected override void Awake()
    {
        _healingSystem = GetComponent<HealingSystem>();

        _owner.OnInstalledEvent += OnInstalled;
    }
    private void OnDestroy()
    {
        _owner.OnInstalledEvent -= OnInstalled;
    }

    private void OnInstalled()
    {
        _healingSystem.Setting();
    }

    protected override void OnMouseEnter()
    {
        //마우스 올렸을때 해당 시너지 이미지 + 빌딩이름 보여주기

        if (!_owner.IsInstalled) return;
        if (!WaveManager.Instance.IsBattlePhase) return;
        if (_healingSystem.IsHealing) return;

        _owner.OutlineCompo.enabled = true;

    }

    protected override void OnMouseExit()
    {
        if (!_owner.IsInstalled) return;
        if (!WaveManager.Instance.IsBattlePhase) return;
        if (_healingSystem.IsHealing) return;

        _owner.OutlineCompo.enabled = false;
    }

    public void SetArmy(Army army)
    {
        if (!_owner.IsInstalled) return;
        if (!WaveManager.Instance.IsBattlePhase) return;
        if (_healingSystem.IsHealing) return;

        _healingSystem.SetArmy(army);

    }

    public override void OnClick()
    {
        if (!_owner.IsInstalled) return;
        if (!WaveManager.Instance.IsBattlePhase) return;
        if (_healingSystem.IsHealing) return;

        _healingSystem.OnClick();

    }
}
