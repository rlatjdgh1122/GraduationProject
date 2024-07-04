using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingDeadController<T> : MonoBehaviour, IDeadable where T : BaseBuilding
{
    protected T _owner;

    protected Collider _colider;

    [SerializeField]
    private Transform _nonBrokenBuilding;
    [SerializeField]
    protected DeadBuilding brokenBuilding;

    public bool IsDie { get; set; }

    private void Awake()
    {
        _owner = GetComponent<T>();
        _colider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += brokenBuilding.BrokenBuilding;
        SignalHub.OnBattlePhaseEndEvent += BuildingCompoOn;
        SignalHub.OnBattlePhaseStartEvent += BuildingCompoOff;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= brokenBuilding.BrokenBuilding;
        SignalHub.OnBattlePhaseEndEvent -= BuildingCompoOn;
        SignalHub.OnBattlePhaseStartEvent -= BuildingCompoOff;
    }

    public virtual void OnDied()
    {
        _owner.InstalledGround()?.UnInstallBuilding();

        IsDie = true;
        
        BuildingCompoOff();

        brokenBuilding.gameObject.SetActive(true);
        _nonBrokenBuilding.gameObject.SetActive(false);
    }

    public void BuildingCompoOff()
    {
        if(IsDie)
        {
            _owner.enabled = false;
            _colider.enabled = false;
        }
    }

    public void BuildingCompoOn()
    {
        _owner.enabled = true;
        _colider.enabled = true;
    }

    public void FixBuilding()
    {
        IsDie = false;

        brokenBuilding.gameObject.SetActive(false);
        _nonBrokenBuilding.gameObject.SetActive(true);
    }

    public void DestroyBuilding()
    {
        _owner.InstalledGround()?.UnInstallBuilding();

        _owner.BuildingItemInfoCompo.CurrentInstallCount -= 1;
        GameObject.Find(_owner.BuildingItemInfoCompo.CodeName).GetComponent<BuildingView>().UpdateUI();

        PoolManager.Instance.Push(_owner);
    }

    public virtual void OnResurrected()
    {

    }
}
