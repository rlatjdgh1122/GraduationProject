using ArmySystem;
using System;
using System.Collections;
using UnityEngine;

public class Penguin : Entity
{

    public float moveSpeed
    {
        get
        {
            if (owner is not null)
            {
                return owner.MoveSpeed;
            }

            return 4f;
        }
    }

    public float attackSpeed = 1f;

    public PassiveDataSO passiveData = null;

    #region property

    public bool ArmyTriggerCalled = false;
    public bool WaitForCommandToArmyCalled = true;
    public bool SuccessfulToArmyCalled = false;


    private Coroutine movingCoroutine = null;
    private Vector3 curMousePos = Vector3.zero;
    private Vector3 prevMousePos = Vector3.zero;
    public Vector3 MousePos
    {
        get => curMousePos;
        set
        {
            prevMousePos = curMousePos;
            curMousePos = value;
        }
    }
    private Vector3 _seatPos = Vector3.zero; //���ܿ��� ��ġ�� �ڸ� OK?
    private float Angle
    {
        get
        {
            Vector3 vec = (curMousePos - prevMousePos);

            if (prevMousePos != Vector3.zero && vec != Vector3.zero)
            {
                float value = Quaternion.FromToRotation(Vector3.forward, vec).eulerAngles.y;
                value = (value > 180f) ? value - 360f : value; // ��ȯ
                return value; // -180 ~ 180
            }
            else //ó�� �������� ��
            {
                Vector3 v = (curMousePos - transform.position);

                float value = Quaternion.FromToRotation(Vector3.forward, v).eulerAngles.y;
                value = (value > 180f) ? value - 360f : value; // ��ȯ
                return value; // -180 ~ 180
            }
        }
    }
    public Vector3 SeatPos
    {
        get
        {
            //Vector3 direction = Quaternion.Euler(0, Angle, 0) * (_seatPos);
            return _seatPos;
        }
        set { _seatPos = value; }
    }
    #endregion

    #region components
    public EntityAttackData AttackCompo { get; private set; }
    public PenguinStateMachine StateMachine { get; private set; }
    #endregion
    public bool IsTargetInInnerRange => CurrentTarget != null && Vector3.Distance(prevMousePos, CurrentTarget.GetClosetPostion(transform.position)) <= innerDistance;



    public Army owner { get; set; }
    public Army MyArmy => owner;

    public MovefocusMode MoveFocusMode
    {
        get
        {
            if (owner != null)
                return owner.MoveFocusMode;

            return MovefocusMode.Battle;
        }
    }

    public bool TargetLock = false; //첫 타겟 그대로 쭉 때리게 할 것인가?

    private void OnEnable()
    {
        SignalHub.OnGroundArrivedEvent += ChaseToTarget;
        SignalHub.OnRaftArrivedEvent += ChaseToTarget;
    }
    private void OnDisable()
    {
        SignalHub.OnGroundArrivedEvent -= ChaseToTarget;
        SignalHub.OnRaftArrivedEvent -= ChaseToTarget;
    }

    protected override void Awake()
    {
        base.Awake();

        if (NavAgent != null)
        {
            NavAgent.speed = moveSpeed;
        }

        AttackCompo = GetComponent<EntityAttackData>();

        if (passiveData != null)
            passiveData = Instantiate(passiveData);
    }

    protected override void Update()
    {
        if (passiveData.IsSecondEvent && CurrentTarget != null)
        {
            passiveData.Update();

            if (CheckSecondPassive())
            {
                OnPassiveSecondEvent();
            }
        }
    }


    protected void SetBaseState()
    {
        StateMachine = new PenguinStateMachine();

        foreach (PenguinStateType state in Enum.GetValues(typeof(PenguinStateType)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Penguin{typeName}State");
            State newState = Activator.CreateInstance(t, this, StateMachine, typeName) as State;
            if (newState == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            StateMachine.AddState(state, newState);
        }
    }

    #region Passive Check
    public bool CheckAttackPassive(int curAttackCount)
=> passiveData?.CheckAttackEventPassive(curAttackCount) ?? false;

    public bool CheckHitPassive(int curHitCount)
=> passiveData?.CheckHitEventPassive(curHitCount) ?? false;

    public bool CheckHealthRatioPassive(float maxHp, float currentHP, float ratio = -1)
 => passiveData?.CheckHealthRatioEventPassive(maxHp, currentHP, ratio) ?? false;

    public bool CheckSecondPassive()
=> passiveData?.CheckSecondEventPassive() ?? false;

    #endregion

    #region Passive Event


    public virtual void OnPassiveAttackEvent() { }

    public virtual void OnPassiveHitEvent() { }

    public virtual void OnPassiveHealthRatioEvent() { }

    public virtual void OnPassiveSecondEvent() { }

    #endregion

    protected void FindTarget()
    {
        FindNearestEnemy();
    }

    public void SetOwner(Army army)
    {
        owner = army;

        if (NavAgent != null)
        {
            NavAgent.speed = moveSpeed;
        }

    }

    #region State Method
    public virtual void AnimationTrigger()
    {

    }

    public virtual void DashEndTrigger()
    {

    }

    public void FindNearestEnemy()
    {
        CurrentTarget = FindNearestTarget<Enemy>(50f, TargetLayer);
    }

    public void ChaseToTarget()
    {
        //적이 빙하가 붙자마자 콜라이더 켜지니깐 켜질때까지 기다리고 찾기
        CoroutineUtil.CallWaitForSeconds(0.1f, null, () => FindNearestEnemy());
    }

    public virtual void LookTarget()
    {
        if (CurrentTarget != null)
        {
            Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
        }
    }

    public virtual void LookTargetImmediately()
    {
        if (CurrentTarget != null)
        {
            Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = targetRotation;
        }
    }
    public virtual void StateInit() { }

    #endregion

    #region Set Stat
    public IEnumerator AddStatCorou(float time, int value, StatType type, StatMode mode)
    {
        yield return new WaitForSeconds(time);
        Stat.AddStat(value, type, mode);
    }

    public IEnumerator RemoveStatCorou(float time, int value, StatType type, StatMode mode,Action completeAction)
    {
        yield return new WaitForSeconds(time);
        Stat.RemoveStat(value, type, mode);

        completeAction?.Invoke();
    }

    #endregion

    #region Move Method

    public void MoveToMySeat(Vector3 mousePos)
    {
        if (NavAgent.isActiveAndEnabled)
        {
            NavAgent.isStopped = false;
            /*
                        if (movingCoroutine != null)
                        {
                            StopCoroutine(movingCoroutine);
                        }*/

            if (prevMousePos != Vector3.zero)
            {
                movingCoroutine = StartCoroutine(Moving());
            }
            else
                MoveToMouseClick(mousePos + SeatPos);
        }
    }
    private IEnumerator Moving()
    {
        float currentTime = 0f;
        float t = 0f;

        Vector3 movePos = Quaternion.Euler(0, Angle, 0) * SeatPos;

        float distance = Vector3.Distance(prevMousePos, curMousePos);
        float totalTime = distance / moveSpeed;

        while (currentTime <= totalTime)
        {
            t = currentTime / totalTime;

            Vector3 frameMousePos = Vector3.Lerp(prevMousePos, curMousePos, t);
            Vector3 finalPos = frameMousePos + movePos;

            MoveToMouseClick(finalPos);

            currentTime += Time.deltaTime;
            yield return null;
        }
        //Debug.Log("움직임 끝");
    }

    private void MoveToMouseClick(Vector3 pos)
    {
        if (NavAgent.isActiveAndEnabled)
        {
            if (float.IsNaN(pos.x) || float.IsNaN(pos.y) || float.IsNaN(pos.z)) return;

            NavAgent.SetDestination(pos);
        }
    }

    public void MoveToMouseClickPositon()
    {
        if (NavAgent != null)
        {
            NavAgent.isStopped = false;
            NavAgent?.SetDestination(MousePos + SeatPos);
        }
    }
    #endregion

    protected override void HandleHit()
    {
    }

    public override void Init()
    {
        base.Init();

        owner = null;
    }


}
