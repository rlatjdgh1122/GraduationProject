using ArmySystem;
using SynergySystem;
using System.Collections;
using System.Collections.Generic;
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
    }

    protected override void OnMouseEnter()
    {
        _owner.OutlineCompo.enabled = true;

        //마우스 올렸을때 해당 시너지 이미지 + 빌딩이름 보여주기
    }

    protected override void OnMouseExit()
    {
        _owner.OutlineCompo.enabled = false;
    }

    public void SetArmy(Army army)
    {
        _healingSystem.SetArmy(army);

    }

    public override void OnClick()
    {
       // _healingSystem.GoToBuilding();
        
    }
}
