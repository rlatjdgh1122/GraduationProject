using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentInitPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PoolableMono obj))
        {
            if(obj.CanInitTent)
            {
                PoolManager.Instance.Push(obj);
            }
        }
    }
}
