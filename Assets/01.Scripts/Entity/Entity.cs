using DG.Tweening.Core.Easing;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public abstract class Entity : PoolableMono
{

    [SerializeField] protected BaseStat _characterStat;
    public BaseStat Stat => _characterStat;

    public T ReturnGenericStat<T>() where T : BaseStat //사실 as랑 같음
    {
        if (_characterStat is T)
        {
            return _characterStat as T;
        }

        Debug.LogError("니가 넣은 스탯 타입이 아니잖아;;");
        return null;
    }

    public float innerDistance = 4f;
    public float attackDistance = 1.5f;

    #region 군단 포지션 관련

    public bool ArmyTriggerCalled = false;
    public bool WaitTrueAnimEndTrigger = true;
    public bool SuccessfulToSeatMyPostion = false;
    public bool BattleMode = false;

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
            if (prevMousePos != Vector3.zero)
            {
                Vector3 vec = (curMousePos - prevMousePos);

                float value = Quaternion.LookRotation(vec).eulerAngles.y;
                value = (value > 180f) ? value - 360f : value; // 변환
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

    #region Components
    public Health HealthCompo { get; private set; }
    public Animator AnimatorCompo { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
    public EntityActionData ActionData { get; private set; }
    public Outline OutlineCompo { get; private set; }

    #endregion

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm?.GetComponent<Animator>(); //이건일단 모르겠어서 ?. 이렇게 해놈
        HealthCompo = GetComponent<Health>();
        NavAgent = GetComponent<NavMeshAgent>();
        OutlineCompo = transform?.GetComponent<Outline>(); //이것도 따로 컴포넌트로 빼야함
        ActionData = GetComponent<EntityActionData>();

        HealthCompo.SetHealth(_characterStat);
        _characterStat = Instantiate(_characterStat);


        if (HealthCompo != null)
        {
            HealthCompo.OnHit += HandleHit;
            HealthCompo.OnDied += HandleDie;
        }
    }

    private void OnDestroy()
    {
        HealthCompo.OnHit -= HandleHit;
        HealthCompo.OnDied -= HandleDie;
    }

    protected virtual void HandleHit()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected abstract void HandleDie();


    #region 움직임 관리
    public void MoveToMySeat(Vector3 mousePos) //싸울때말고 군단 위치로
    {
        MousePos = mousePos;
        if (NavAgent.isActiveAndEnabled)
        {
            if (prevMousePos != Vector3.zero)
            {
                StartCoroutine(Moving());
            }
            else
                MoveToTarget(mousePos + SeatPos);
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
        float result = Mathf.Sqrt(BC); //이게 클수록 수는 쭐어야함

        totalTime = result / balancingValue;
        Debug.Log("처음 :" + totalTime);

        while (currentTime <= totalTime)
        {
            t = currentTime / totalTime;

            Vector3 frameMousePos = Vector3.Lerp(prevMousePos, curMousePos, t);

            Vector3 finalPos = frameMousePos + movePos;

            MoveToTarget(finalPos);

            currentTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("끝 :" + Angle);
        Vector3 pos = MousePos + movePos; // 미리 계산된 회전 위치를 여기에서 사용
        MoveToTarget(pos);
    }

    public void SetTarget(Vector3 mousePos)
    {
        if (NavAgent.isActiveAndEnabled)
        {
            MoveToTarget(mousePos);
        }
    }

    public Vector3 GetSeatPosition() => MousePos + SeatPos;


    public void MoveToTarget(Vector3 pos)
    {
        NavAgent.ResetPath();
        NavAgent.SetDestination(pos);
    }

    public void StopImmediately()
    {
        if (NavAgent.isActiveAndEnabled)
        {
            NavAgent.isStopped = true;
            NavAgent.velocity = Vector3.zero; //미끄러짐 방지
        }
    }
    #endregion
}
