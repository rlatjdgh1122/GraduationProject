using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : Entity
{
    public ResourceObject Target;
    public Transform Nexus;

    public bool CanWork = false;
    public bool EndWork = false;

    #region components
    public Animator WorkerAnimatorCompo { get; private set; }
    public NavMeshAgent WorkerNavAgent { get; private set; }
    public DamageCaster WorkerDamageCasterCompo { get; private set; }
    #endregion

    protected override void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        Nexus = GameManager.Instance.NexusTrm;
        WorkerAnimatorCompo = visualTrm.GetComponent<Animator>();
        WorkerNavAgent = GetComponent<NavMeshAgent>();
        WorkerDamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();

        WorkerDamageCasterCompo.SetOwner(this);
    }

    #region 이동 관련
    public void MoveToTarget()
    {
        WorkerNavAgent.SetDestination(Target.transform.position);
        Debug.Log("이동 중");
    }

    public float CheckDistance()
    {
        return Vector3.Distance(transform.position, Target.transform.position);
    }

    public void MoveToNexus()
    {
        WorkerNavAgent.SetDestination(Nexus.transform.position);
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
    #endregion

    #region 작업 관련
    public void StartWork(ResourceObject target)
    {
        Target = target;
        CanWork = true;
    }

    public void FinishWork()
    {
        CanWork = false;
        EndWork = true;
    }

    public void HitResource()
    {
        WorkerDamageCasterCompo.CastDamage();
    }
    #endregion

    public void LookTaget()
    {
        Vector3 directionToTarget = Target.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
    }

    public virtual void AnimationTrigger()
    {

    }

    protected override void HandleDie()
    {
        
    }
}
