using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingDeadController<T> : MonoBehaviour, IDeadable where T : BaseBuilding
{
    protected T _owner;

    private void Awake()
    {
        _owner = GetComponent<T>();
    }

    public virtual void OnDied()
    {

    }

    public virtual void OnResurrected()
    {

    }
}
