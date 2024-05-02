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

    public PassiveDataSO passiveData = null;
    #region componenets
    public EntityAttackData AttackCompo { get; private set; }
    private IDeadable _deadCompo = null;
    #endregion
    public Transform NexusTarget = null;

    public bool IsMove = false;
    public bool IsProvoked = false;
    public bool IsTargetNexus = false;
    public bool UseAttackCombo = false;

    public bool IsTargetInInnerRange => CurrentTarget != null &&
                            Vector3.Distance(transform.position, CurrentTarget.GetClosetPostion(transform)) <= innerDistance;
    public bool IsTargetInAttackRange => CurrentTarget != null &&
                            Vector3.Distance(transform.position, CurrentTarget.GetClosetPostion(transform)) <= attackDistance;

    public EnemyStateMachine StateMachine { get; private set; }

    private void OnEnable()
    {
        NexusTarget = GameManager.Instance.NexusTrm;
    }
    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine();

        foreach (EnemyStateType state in Enum.GetValues(typeof(EnemyStateType)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Enemy{typeName}State");
            try
            {
                EnemyState newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState;

                StateMachine.AddState(state, newState);

            }
            catch
            {
                Debug.LogError($"There is no script : {state}");

            }
        }

        if (NavAgent != null)
        {
            NavAgent.speed = moveSpeed;
        }

        AttackCompo = GetComponent<EntityAttackData>();
        _deadCompo = GetComponent<IDeadable>();

        if (passiveData != null)
            passiveData = Instantiate(passiveData);
    }

    protected override void Start()
    {
        base.Start();

        HealthCompo.OnHit += FindTarget;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        HealthCompo.OnHit -= FindTarget;
    }

    private void FindTarget()
    {
        if (IsTargetNexus)
        {
            //³ª ¶§¸°³ðÀÌ Å¸°Ù
            SetTarget(ActionData.HitTarget);
        }
        else
        {
            FindNearestTarget();
        }
    }

   


    public void FindNearestTarget()
    {
        CurrentTarget = FindNearestTarget<TargetObject>(innerDistance, TargetLayer);
        IsTargetNexus = false;

        //target is not Nexus
        if (CurrentTarget != null && CurrentTarget is not Nexus) return;

        //set target to Nexus
        CurrentTarget = FindNearestTarget<Nexus>(50f, TargetLayer);
        IsTargetNexus = true;

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
    => passiveData?.CheckAttackEventPassive(curAttackCount) ?? false;

    public virtual void OnPassiveAttackEvent()
    {

    }
    public bool CheckStunEventPassive(float maxHp, float currentHP)
    => passiveData?.CheckStunEventPassive(maxHp, currentHP) ?? false;

    public virtual void OnPassiveStunEvent()
    {

    }
    #endregion
}
