using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Worker : Entity
{
    #region components
    public EntityAttackData AttackCompo { get; private set; }
    public Transform WorkerHomeTrm;
    #endregion

    public bool CanWork = false;
    public bool EndWork;

    public bool WorkerStateCheck = false;

    protected override void Awake()
    {
        base.Awake();

        AttackCompo = GetComponent<EntityAttackData>();

        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();

        WorkerHomeTrm = GameManager.Instance.WorkerSpawnPoint;

        DamageCasterCompo.SetOwner(this);
        Init();
    }

    #region 이동 관련
    public void MoveToTarget()
    {
        NavAgent.SetDestination(CurrentTarget.transform.position);
    }

    public float CheckDistance()
    {
        //Debug.Log(Vector3.Distance(transform.position, CurrentTarget.GetClosetPostion(transform.position)));
        return Vector3.Distance(transform.position, CurrentTarget.GetClosetPostion(transform.position));
    }

    public void MoveToNexus()
    {
        NavAgent.SetDestination(WorkerHomeTrm.transform.position);
    }

    public void WorkerStateChange()
    {
        WorkerStateCheck = true;
    }

    public float CheckNexusDistance()
    {
        return Vector3.Distance(transform.position, WorkerHomeTrm.transform.position);
    }

    public void MoveEndToNexus()
    {
        CanWork = false;
        PoolManager.Instance.Push(this);
        //gameObject.SetActive(false);
    }
    #endregion

    #region 작업 관련
    public void StartWork(WorkableObject target)
    {
        CurrentTarget = target;
        CanWork = true;
    }

    public void FinishWork()
    {
        CanWork = false;
        EndWork = true;
    }
    #endregion

    public void LookTaget()
    {
        Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
    }

    public void ChangeNavqualityToNone() //Nave Quality None으로 변경
    {
        NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    public void ChangeNavqualityToHigh() //Nave Quality HighQuality로 변경
    {
        NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }

    public virtual void AnimationTrigger()
    {

    }

    protected override void HandleDie()
    {
        
    }

    public override void Init()
    {

        CanWork = false;
        EndWork = false;
    }
}
