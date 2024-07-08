using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingDeadController<T> : MonoBehaviour, IDeadable where T : BaseBuilding
{
    protected T _owner;

    protected Collider _colider;

    [SerializeField] protected Transform nonBrokenBuilding;
    [SerializeField] protected DeadBuilding brokenBuilding;

    protected virtual void Awake()
    {
        _owner = GetComponent<T>();
        _colider = GetComponent<Collider>();
    }    

    public virtual void OnDied()
    {
        SetBuildingCondition(true);

        _owner.enabled = false;
        _colider.enabled = false;
    }

    public void DestroyBuilding()
    {
        ResetUI();

        ResetBuilding();

        PoolManager.Instance.Push(_owner);
    }

    protected virtual void SetBuildingCondition(bool isDead)
    {
        nonBrokenBuilding.gameObject.SetActive(!isDead);
        brokenBuilding.gameObject.SetActive(isDead);
    }

    protected virtual void BuildingCompoOff()
    {
        if (_owner.HealthCompo.IsDead)
        {
            _owner.enabled = false;
            _colider.enabled = false;
        }
    }

    protected void BuildingCompoOn()
    {
        _owner.enabled = true;
        _colider.enabled = true;
    }

    private void ResetUI()
    {
        _owner.BuildingItemInfoCompo.CurrentInstallCount -= 1;
        GameObject.Find(_owner.BuildingItemInfoCompo.CodeName).GetComponent<BuildingView>().UpdateUI();
    }

    protected virtual void ResetBuilding()
    {
        _owner.InstalledGround()?.UnInstallBuilding();

        SetBuildingCondition(false);
        BuildingCompoOn();

        _owner.ResetNoise();
        _owner.HealthCompo.ResetHealth();
    }

    public virtual void OnResurrected()
    {

    }
}
