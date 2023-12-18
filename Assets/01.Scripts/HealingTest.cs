using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTest : MonoBehaviour
{
    [SerializeField] private float _rangeSize;
    [SerializeField] private LayerMask _healingTargetLayer;
    [SerializeField] private Collider[] _colls;
    private void Update()
    {
        Physics.OverlapSphereNonAlloc(transform.position, _rangeSize, _colls, _healingTargetLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _rangeSize);
    }
}
