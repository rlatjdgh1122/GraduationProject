using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Penguin : Entity
{
    public float moveSpeed = 4.5f;
    public float attackSpeed = 1f;
    public int maxDetectedCount;
    public float provokeRange = 25f;

    public PassiveDataSO passiveData = null;

    public bool isDummyPenguinMode = false;
    public bool IsDummyPenguinMode
    {
        get => isDummyPenguinMode;
        set
        {
            if (value == isDummyPenguinMode) return;
            isDummyPenguinMode = value;
            if (isDummyPenguinMode)
            {
                OnFreelyMode();
            }
            else
            {
                UnFreelyMode();
            }
        }
    }


    protected virtual void OnFreelyMode() { }
    public virtual void UnFreelyMode() { }

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
            if (prevMousePos != Vector3.zero
                && vec != Vector3.zero)
            {
                //float value = Mathf.Atan2(vec.z, vec.x) * Mathf.Rad2Deg;
                //float value = Quaternion.FromToRotation(Vector3.forward, vec).eulerAngles.y;
                float value = Quaternion.LookRotation(vec).eulerAngles.y;
                //value = (value > 180f) ? value - 360f : value; // 변환
                return value; // -180 ~ 180
            }
            else
                return 0;
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
    #endregion

    #region 더미 펭귄 관련
    //애니메이션이 늘어날때마다 추가
   
    #endregion

    //public Enemy CurrentTarget;

    //public bool IsDead = false;
    public bool IsInnerTargetRange => CurrentTarget != null && Vector3.Distance(MousePos, CurrentTarget.transform.position) <= innerDistance;
    public bool IsInnerMeleeRange => CurrentTarget != null && Vector3.Distance(transform.position, CurrentTarget.transform.position) <= attackDistance;

    private Army owner;

    public Army Owner => owner;

    private bool isFreelyMove = false;
    public bool IsFreelyMove => isFreelyMove;
    protected override void Awake()
    {
        base.Awake();

        if (NavAgent != null)
        {
            NavAgent.speed = moveSpeed;
        }
        AttackCompo = GetComponent<EntityAttackData>();
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
            .Where(obj => Vector3.Distance(transform.position, obj.transform.position) <= provokeRange)
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

    protected override void HandleDie()
    {
        IsDead = true;

        //사실 이런 경우가 생기면 안되는데 더미펭귄이 실제 펭귄이라서 같이 싸울때가 있기에 예외처리
        if (Owner != null)
        {
            ArmyManager.Instance.Remove(Owner.Legion, this);
        }
        SignalHub.OnModifyArmyInfo?.Invoke();
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


    public void MoveToMySeat(Vector3 mousePos) //싸울때말고 군단 위치로
    {
        if (NavAgent.isActiveAndEnabled)
        {
            if (prevMousePos != Vector3.zero)
            {
                if (movingCoroutine != null)
                    StopCoroutine(movingCoroutine);

                movingCoroutine = StartCoroutine(Moving());
            }
            else
                MoveToMouseClick(mousePos + SeatPos);
        }
    }
    float totalTime = 1f; // 총 시간 (1초로 가정)
    float balancingValue = 10f;
    float currentTime = 0f; // 현재 시간

    private IEnumerator Moving()
    {
        currentTime = 0f;
        float t = 0f;

        float AC = Vector3.Distance(MousePos, SeatPos);
        Vector3 movePos = Quaternion.Euler(0, Angle, 0) * SeatPos;
        float AB = Vector3.Distance(MousePos, movePos);

        float BC =
            Mathf.Pow(AB, 2) + Mathf.Pow(AC, 2) - (2 * AC * AB) * Mathf.Cos(Angle);
        //마우스 위치부터 나의 위치와 움직일 위치에 거리
        float result = Mathf.Sqrt(BC); //이게 클수록 수는 작게

        totalTime = result / balancingValue;

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

    public void SetTarget(Vector3 mousePos)
    {
        MoveToPosition(mousePos);
    }
    public void MoveToMouseClickPositon()
    {
        //StartImmediately();
        NavAgent.isStopped = false;
        NavAgent?.SetDestination(MousePos + SeatPos);
    }
    private void MoveToMouseClick(Vector3 pos)
    {
        if (NavAgent.isActiveAndEnabled)
        {
            NavAgent.SetDestination(pos);
        }
    }
    #endregion

    #region 명령에 따른 함수 관련

    #endregion

    public override void Init()
    {
        owner = null;
    }

    public void SetFreelyMoveAble(bool b)
    {
        isFreelyMove = b;
    }

}
