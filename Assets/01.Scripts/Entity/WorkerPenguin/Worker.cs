using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public Transform Target;

    public Animator AnimatorCompo { get; private set; }
    public NavMeshAgent NavAgentCompo { get; private set; } 

    private void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        NavAgentCompo = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget()
    {
        NavAgentCompo.SetDestination(Target.transform.position);
    }

    public float CheckDistance()
    {
        return Vector3.Distance(transform.position, Target.transform.position);
    }

    public virtual void AnimationTrigger()
    {

    }
}
