using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : Entity
{
    public Transform Target;
    public Transform Nexus;

    public bool CanWork = false;
    public bool EndWork = false;

    #region components
    public new Animator AnimatorCompo { get; private set; }
    public new NavMeshAgent NavAgent { get; private set; }
    public new DamageCaster DamageCasterCompo { get; private set; }
    #endregion

    protected override void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        Nexus = GameManager.Instance.NexusTrm;
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        NavAgent = GetComponent<NavMeshAgent>();
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();

        DamageCasterCompo.SetOwner(this);
    }

    #region 이동 관련
    public void MoveToTarget()
    {
        NavAgent.SetDestination(Target.transform.position);
        Debug.Log("이동 중");
    }

    public float CheckDistance()
    {
        return Vector3.Distance(transform.position, Target.transform.position);
    }

    public void MoveToNexus()
    {
        NavAgent.SetDestination(Nexus.transform.position);
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

    public void StartWork(Transform target)
    {
        Target = target;
        CanWork = true;
    }

    public void HitResource()
    {
        DamageCasterCompo.CastDamage();
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

    protected override void HandleDie()
    {
        throw new System.NotImplementedException();
    }
}
