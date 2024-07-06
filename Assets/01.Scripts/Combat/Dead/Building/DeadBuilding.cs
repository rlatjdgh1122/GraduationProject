using System;
using UnityEngine;

public abstract class DeadBuilding : MonoBehaviour
{
    protected BaseBuilding parentBuilding;
    public BaseBuilding ParentBuilding => parentBuilding;

    public Action OnPushBuildingEvent = null;

    protected virtual void Awake()
    {
        parentBuilding = transform.parent.GetComponent<BaseBuilding>();
    }

    public abstract void BrokenBuilding();

    public virtual void PushBuilding()
    {
        OnPushBuildingEvent?.Invoke();
    }
}