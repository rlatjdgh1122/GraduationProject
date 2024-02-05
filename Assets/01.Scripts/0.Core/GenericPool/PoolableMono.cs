using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour
{
    private bool canInitTent = false;
    public bool CanInitTent => canInitTent;

    public void SetCanInitTent(bool can)
    {
        canInitTent = can;
    }

    public virtual void Init()
    {

    }
}
