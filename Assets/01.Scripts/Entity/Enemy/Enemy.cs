using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyDeadController))]
public class Enemy : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 3f;
    public float attackSpeed = 1f;
    public float rotationSpeed = 2f;

    [SerializeField]
    [Range(0.1f, 6f)]
    protected float nexusDistance;

    public PassiveDataSO passiveData = null;
    #region componenets
    public EntityAttackData AttackCompo { get; private set; }
    private IDeadable _deadCompo = null;
    #endregion
    public Transform NexusTarget;

    public bool IsMove = false;
    public bool IsProvoked = false;

    public bool IsTargetPlayerInsideWhenNexus => CurrentTarget != null &&
                           Vector3.Distance(transform.position, CurrentTarget.transform.position) <= 500;
    public bool IsTargetPlayerInside => CurrentTarget != null &&
                            Vector3.Distance(transform.position, CurrentTarget.transform.position) <= innerDistance;
    public bool CanAttack => CurrentTarget != null &&
                            Vector3.Distance(transform.position, CurrentTarget.transform.position) <= attackDistance;
    public bool IsReachedNexus =>
                            Vector3.Distance(transform.position, NexusTarget.position) <= nexusDistance;


    protected override void Awake()
    {
        base.Awake();
        NavAgent.speed = moveSpeed;

        AttackCompo = GetComponent<EntityAttackData>();
        _deadCompo = GetComponent<IDeadable>();

    }
    private void OnEnable()
    {
        SignalHub.OnIceArrivedEvent += FindNearestPenguin;
        NexusTarget ??= GameManager.Instance.NexusTrm;
    }

    private void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= FindNearestPenguin;
    }

    public void FindNearestPenguin()
    {
        CurrentTarget = FindNearestTarget<TargetObject>(TargetLayer);
    }

    protected override void HandleDie()
    {
        _deadCompo.OnDied();
    }

    public virtual void AnimationTrigger()
    {

    }

    public void MoveToNexus()
    {
        if (NavAgent != null)
        {
            NavAgent.ResetPath();
            NavAgent.SetDestination(NexusTarget.position);
        }
    }

    public void LookAtNexus()
    {
        if (NexusTarget != null)
        {
            Vector3 directionToTarget = NexusTarget.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void LookTarget()
    {
        if (CurrentTarget != null)
        {
            Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;

            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
    private void FriendlyPenguinDeadHandler()
    {
        WaveManager.Instance.CheckIsEndBattlePhase();
        SignalHub.OnEnemyPenguinDead -= FriendlyPenguinDeadHandler;
    }

    public void DieEventHandler()
    {
        SignalHub.OnEnemyPenguinDead += FriendlyPenguinDeadHandler;
        SignalHub.OnEnemyPenguinDead?.Invoke();
    }

    #region passive
    public bool CheckAttackEventPassive(int curAttackCount)
=> passiveData.CheckAttackEventPassive(curAttackCount);

    public virtual void OnPassiveAttackEvent()
    {

    }
    public bool CheckStunEventPassive(float maxHp, float currentHP)
 => passiveData.CheckStunEventPassive(maxHp, currentHP);

    public virtual void OnPassiveStunEvent()
    {

    }
    #endregion
}
