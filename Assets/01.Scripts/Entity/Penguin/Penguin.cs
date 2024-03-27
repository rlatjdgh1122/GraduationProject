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
            if (prevMousePos != Vector3.zero
                && vec != Vector3.zero)
            {
                //float value = Mathf.Atan2(vec.z, vec.x) * Mathf.Rad2Deg;
                //float value = Quaternion.FromToRotation(Vector3.forward, vec).eulerAngles.y;
                float value = Quaternion.LookRotation(vec).eulerAngles.y;
                //value = (value > 180f) ? value - 360f : value; // ��ȯ
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

    #region ���� ��� ����
    //�ִϸ��̼��� �þ������ �߰�
   
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

        //��� �̷� ��찡 ����� �ȵǴµ� ��������� ���� ����̶� ���� �οﶧ�� �ֱ⿡ ����ó��
        if (Owner != null)
        {
            ArmyManager.Instance.Remove(Owner.Legion, this);
        }
        SignalHub.OnModifyArmyInfo?.Invoke();
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


    public void MoveToMySeat(Vector3 mousePos) //�οﶧ���� ���� ��ġ��
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
    float totalTime = 1f; // �� �ð� (1�ʷ� ����)
    float balancingValue = 10f;
    float currentTime = 0f; // ���� �ð�

    private IEnumerator Moving()
    {
        currentTime = 0f;
        float t = 0f;

        float AC = Vector3.Distance(MousePos, SeatPos);
        Vector3 movePos = Quaternion.Euler(0, Angle, 0) * SeatPos;
        float AB = Vector3.Distance(MousePos, movePos);

        float BC =
            Mathf.Pow(AB, 2) + Mathf.Pow(AC, 2) - (2 * AC * AB) * Mathf.Cos(Angle);
        //���콺 ��ġ���� ���� ��ġ�� ������ ��ġ�� �Ÿ�
        float result = Mathf.Sqrt(BC); //�̰� Ŭ���� ���� �۰�

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
        Vector3 pos = MousePos + movePos; // �̸� ���� ȸ�� ��ġ�� ���⿡�� ���
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

    #region ��ɿ� ���� �Լ� ����

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
