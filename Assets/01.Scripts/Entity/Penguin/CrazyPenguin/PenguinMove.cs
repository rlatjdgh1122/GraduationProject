using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PenguinMove : MonoBehaviour
{
    private bool _isMoveEnd = false;
    public bool IsReturn = false;

    private NavMeshAgent _navAgent;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPosition(Vector3 target)
    {
        StopAllCoroutines();

        _isMoveEnd = false;

        if (_navAgent.isActiveAndEnabled)
        {
            _navAgent?.SetDestination(target);
        }

        StartCoroutine(OnMove());
    }

    private IEnumerator OnMove()
    {
        while (true)
        {
            if (Vector3.Distance(_navAgent.destination, transform.position) < 0.1f)
            {
                transform.position = _navAgent.destination;
                _navAgent.ResetPath();

                break;
            }

            yield return null;
        }

        _isMoveEnd = true;
    }

    public bool MoveEnd() => _isMoveEnd;
}
