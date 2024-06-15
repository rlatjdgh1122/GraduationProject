using ArmySystem;
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

    public EnemyArmy _owner { get; set; }

    public EnemyArmy MyArmy => _owner;

    #region componenets
    public EntityAttackData AttackCompo { get; private set; }
    #endregion

    public Nexus NexusTarget = null;

    public bool IsMove = false;
    public bool IsProvoked = false;
    public bool UseAttackCombo = false;

    public bool IsTargetInInnerRangeWhenTargetNexus => NexusTarget != null &&
                           Vector3.Distance(transform.position, NexusTarget.GetClosetPostion(transform.position)) <= 100;

    public bool IsTargetInInnerRange => CurrentTarget != null &&
                            Vector3.Distance(transform.position, CurrentTarget.GetClosetPostion(transform.position)) <= innerDistance;
    public bool IsReachedNexus =>
                            Vector3.Distance(transform.position, NexusTarget.GetClosetPostion(transform.position)) <= nexusDistance;

    public EnemyStateMachine StateMachine { get; private set; }

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
        NexusTarget = GameManager.Instance.NexusTrm.GetComponent<Nexus>();

        if (passiveData != null)
            passiveData = Instantiate(passiveData);
    }

    public void StateInit()
    {
        StateMachine.Init(EnemyStateType.Idle);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void FindNearestTarget()
    {
        CurrentTarget = FindNearestTarget<TargetObject>(100, TargetLayer);
    }

    public virtual void AnimationTrigger()
    {

    }

    protected override void HandleHit()
    {

    }


    public void MoveToNexus()
    {
        if (NavAgent.isActiveAndEnabled)
        {
            //NavAgent.ResetPath();
            NavAgent.isStopped = false;
            NavAgent.SetDestination(NexusTarget.transform.position);
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

    public void DieEventHandler()
    {
        CoroutineUtil.CallWaitForSeconds(0.1f, () => WaveManager.Instance.CheckIsEndBattlePhase());
    }

    #region passive
    public bool CheckAttackEventPassive(int curAttackCount)
    => passiveData?.CheckAttackEventPassive(curAttackCount) ?? false;

    public virtual void OnPassiveAttackEvent()
    {

    }
    public bool CheckStunEventPassive(float maxHp, float currentHP)
    => passiveData?.CheckHealthRatioEventPassive(maxHp, currentHP) ?? false;

    public virtual void OnPassiveStunEvent()
    {

    }


    #endregion

    #region Army

    public void JoinEnemyArmy(EnemyArmy army)
    {
        _owner = army;
    }

    #endregion
}
