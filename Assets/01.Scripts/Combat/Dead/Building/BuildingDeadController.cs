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
    private DeadBuillding _brokenBuilding;

    private void Awake()
    {
        _owner = GetComponent<T>();
        _colider = GetComponent<Collider>();
    }

    public virtual void OnDied()
    {
        _owner.InstalledGround()?.UnInstallBuilding();

        _owner.enabled = false;
        _colider.enabled = false;

        _brokenBuilding.gameObject.SetActive(true);
        _nonBrokenBuilding.gameObject.SetActive(false);

        _owner.BuildingItemInfoCompo.CurrentInstallCount -= 1;
        GameObject.Find(_brokenBuilding.ParentBuilding.BuildingItemInfoCompo.CodeName).GetComponent<BuildingView>().UpdateUI();
    }

    public virtual void OnResurrected()
    {

    }
}
