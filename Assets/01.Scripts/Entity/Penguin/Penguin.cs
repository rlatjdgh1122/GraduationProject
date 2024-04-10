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

    #region 군단 포지션 관련

    public bool ArmyTriggerCalled = false;
    public bool WaitForCommandToArmyCalled = true; //군단의 명령을 들을 수 있을때까지 대기
    public bool SuccessfulToArmyCalled = false; //군단의 명령을 성공적으로 해결했는가
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
    private Vector3 _seatPos = Vector3.zero; //군단에서 배치된 자리 OK?
    private float Angle
    {
        get
        {
            Vector3 vec = (curMousePos - prevMousePos);

            if (prevMousePos != Vector3.zero && vec != Vector3.zero)
            {
                float value = Quaternion.FromToRotation(Vector3.forward, vec).eulerAngles.y;
                value = (value > 180f) ? value - 360f : value; // 변환
                return value; // -180 ~ 180
            }
            else //처음 움직였을 때
            {
                Vector3 v = (curMousePos - transform.position);

                float value = Quaternion.FromToRotation(Vector3.forward, v).eulerAngles.y;
                value = (value > 180f) ? value - 360f : value; // 변환
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

    #region 일반 병사들 패시브
    //General에서 뺴옴 ㅋ
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

    #region AI 관련
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

    #region 스탯 관련
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

    #region 움직임 관련
    //배틀모드일때 다죽이고 마지막 마우스 위치로 이동 코드

    /// <summary>
    /// 배치된 위치로 이동
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
        Vector3 pos = MousePos + movePos; // 미리 계산된 회전 위치를 여기에서 사용
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

    #region 명령에 따른 함수 관련

    #endregion

    #region 더미 펭귄 스왑 관련
    private void ChangedToDummyPenguinHandler()
    {
        //켜져있는 애들만
        if (gameObject.activeSelf)
        {
            //안죽었다면 더미펭귄으로 변신
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

    //근데 스크립트가 꺼져있는데 이 함수가 호출이 되나?
    public override void Init()
    {
        owner = null;
        _liveCompo.OnResurrected();
    }

}
