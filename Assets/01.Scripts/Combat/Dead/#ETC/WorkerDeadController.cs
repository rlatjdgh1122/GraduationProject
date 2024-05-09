using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerDeadController : MonoBehaviour, IDeadable
{
    private Worker _owner = null;
    private NavMeshAgent _navAgent = null;
    private void Awake()
    {
        _owner = GetComponent<Worker>();
        _navAgent = GetComponent<NavMeshAgent>();
    }
    public void OnDied()
    {
        
    }

    public void OnResurrected()
    {
        _navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
        _navAgent.avoidancePriority = 0;

        _owner.CanWork = false;
        _owner.EndWork = false;
    }
}
