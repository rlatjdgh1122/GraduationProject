using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    public Transform Target;
    public Transform Nexus;

    public bool CanWork = false;
    public bool EndWork = false;

    public Animator AnimatorCompo { get; private set; }
    public NavMeshAgent NavAgentCompo { get; private set; } 

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        NavAgentCompo = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget()
    {
        NavAgentCompo.SetDestination(Target.transform.position);
        Debug.Log("이동 중");
    }

    public float CheckDistance()
    {
        return Vector3.Distance(transform.position, Target.transform.position);
    }

    public void MoveToNexus()
    {
        NavAgentCompo.SetDestination(Nexus.transform.position);
    }

    public float CheckNexusDistance()
    {
        return Vector3.Distance(transform.position, Nexus.transform.position);
    }

    public void MoveEndToNexus()
    {
        CanWork = false;
        gameObject.SetActive(false);
    }

    public void Work()
    {
        //work 내용 채워서 사용 현재는 2초뒤 State가 Work로 변경 되도록 해놓음
        StartCoroutine(Working());
    }
    
    IEnumerator Working()
    {
        yield return new WaitForSeconds(2f);
        EndWork = true;
    }

    public void LookTaget()
    {
        Vector3 directionToTarget = Target.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
    }

    public virtual void AnimationTrigger()
    {

    }
}
