using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PenguinDeadController))]
public class Penguin : Entity
{
    public float moveSpeed = 4.5f;
    public float attackSpeed = 1f;
    public int maxDetectedCount;
    public float provokeRange = 25f;

    public PassiveDataSO passiveData = null;

    #region ���� ������ ����

    public bool ArmyTriggerCalled = false;
    public bool WaitForCommandToArmyCalled = true; //������ ����� ���� �� ���������� ���
    public bool SuccessfulToArmyCalled = false; //������ ����� ���������� �ذ��ߴ°�
    public MovefocusMode MoveFocusMode => ArmyManager.Instance.CurFocusMode;

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
    private IDeadable _deadCompo = null;
    private ILiveable _liveCompo = null;
    #endregion
    public bool IsInnerTargetRange => CurrentTarget != null && Vector3.Distance(MousePos, CurrentTarget.transform.position) <= innerDistance;
    public bool IsInnerMeleeRange => CurrentTarget != null && Vector3.Distance(transform.position, CurrentTarget.transform.position) <= attackDistance;

    private Army owner;
    public Army MyArmy => owner;

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += ChangedToDummyPenguinHandler;
    }
    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= ChangedToDummyPenguinHandler;
    }
    protected override void Awake()
    {
        base.Awake();

        if (NavAgent != null)
        {
            NavAgent.speed = moveSpeed;
        }
        AttackCompo = GetComponent<EntityAttackData>();
        _deadCompo = GetComponent<IDeadable>();
        _liveCompo = GetComponent<ILiveable>();
    }

    #region �Ϲ� ����� �нú�
    //General���� ���� ��
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

    public void SetOwner(Army army)
    {
        owner = army;
    }

    #region AI ����
    public virtual void AnimationTrigger()
    {

    }
    public void FindFirstNearestEnemy()
    {
        CurrentTarget = FindNearestEnemy().FirstOrDefault();
    }

    public List<Enemy> FindNearestEnemy(int count = 1)
    {
        Enemy[] objects = FindObjectsOfType<Enemy>().Where(e => e.enabled).ToArray();

        var nearbyEnemies = objects
            .OrderBy(obj => Vector3.Distance(transform.position, obj.transform.position))
            .Take(count)
            .ToList();

        return nearbyEnemies;
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

    #endregion
    protected override void HandleDie()
    {
        _deadCompo.OnDied();
    }

    #region ���� ����
    public void AddStat(int value, StatType type, StatMode mode)
    {
        Stat.AddStat(value, type, mode);
    }
    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        Stat.RemoveStat(value, type, mode);
    }

    public IEnumerator AddStatCorou(float time, int value, StatType type, StatMode mode)
    {
        yield return new WaitForSeconds(time);
        Stat.AddStat(value, type, mode);
    }
    public IEnumerator RemoveStatCorou(float time, int value, StatType type, StatMode mode)
    {
        yield return new WaitForSeconds(time);
        Stat.RemoveStat(value, type, mode);
    }

    #endregion

    #region ������ ����
    //��Ʋ����϶� �����̰� ������ ���콺 ��ġ�� �̵� �ڵ�

    /// <summary>
    /// ��ġ�� ��ġ�� �̵�
    /// </summary>
    /// <param name="mousePos"></param>
    public void MoveToMySeat(Vector3 mousePos)
    {
        if (NavAgent.isActiveAndEnabled)
        {
            NavAgent.isStopped = false;

            if (movingCoroutine != null)
                StopCoroutine(movingCoroutine);

            movingCoroutine = StartCoroutine(Moving());

            /* if (prevMousePos != Vector3.zero)
             {
             }
             else
                 MoveToMouseClick(mousePos + SeatPos);*/
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
        Vector3 pos = MousePos + movePos; // �̸� ���� ȸ�� ��ġ�� ���⿡�� ���
        MoveToMouseClick(pos);
    }
    private void MoveToMouseClick(Vector3 pos)
    {
        if (NavAgent.isActiveAndEnabled)
        {
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

    #region ��ɿ� ���� �Լ� ����

    #endregion

    #region ���� ��� ���� ����
    private void ChangedToDummyPenguinHandler()
    {
        //�����ִ� �ֵ鸸
        if (gameObject.activeSelf)
        {
            //���׾��ٸ� ����������� ����
            if (!IsDead)
                SpawnManager.Instance.ChangedToDummyPenguin(this);
        }
    }
    public void SetPosAndRotation(Transform trm)
    {
        transform.position = trm.position;
        transform.rotation = trm.rotation;
    }

    public virtual void StateInit() { }

    #endregion

    //�ٵ� ��ũ��Ʈ�� �����ִµ� �� �Լ��� ȣ���� �ǳ�?
    public override void Init()
    {
        owner = null;
        _liveCompo.OnResurrected();
    }

}
