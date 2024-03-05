using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

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

    #region 군단 포지션

    private bool isAbsoluteMovement = false;
    public bool WaitTrueAnimEndTrigger = true;
    public bool ArmyTriggerCalled
    {
        get { return isAbsoluteMovement; }
        set { isAbsoluteMovement = value; }
    }

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

                float value = Quaternion.FromToRotation(Vector3.forward, vec).eulerAngles.y;
                //float result = Mathf.Floor(value);
                return value; //0 ~ 360
            }
            else
                return 0;
        }
    }

    public Vector3 SeatPos
    {
        get
        {
            Vector3 direction = Quaternion.Euler(0, Angle, 0) * (_seatPos);
            return direction;
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
            MoveToTarget(mousePos + SeatPos);
        }
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
            NavAgent.isStopped = true;
    }
    #endregion
}
