using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : PoolableMono
{

    public float innerDistance = 4f;
    public float attackDistance = 1.5f;

    [SerializeField] protected Arrow _arrowPrefab;
    [SerializeField] protected Transform _firePos;

    #region 패시브

    //몇대 때릴때마다
    public bool IsAttackEvent = false;
    public int AttackCount = 3;

    //몇 초 마다
    public bool IsSecondEvent = false;
    public float EverySecond = 10f;

    //뒤에서 때릴때
    public bool IsBackAttack = false;

    //범위 안에 주변의 적이 몇명인가
    public bool IsAroundEnemyCountEventEvent = false;
    public float AroundRadius = 3;
    public int AroundEnemyCount = 3;

    public PassiveDataSO passiveData = null;

    #region 군단 포지션

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
            Debug.Log(Angle + " : " + direction);
            return direction;
        }
        set { _seatPos = value; }
    }
    #endregion

    #region Components
    public Animator AnimatorCompo { get; private set; }
    public Health HealthCompo { get; private set; }
    public DamageCaster DamageCasterCompo { get; private set; }
    public CharacterController CharController { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
    public EntityActionData ActionData { get; private set; }
    public Outline OutlineCompo { get; private set; }

    [SerializeField] protected SkinnedMeshRenderer _renderer;
    public SkinnedMeshRenderer Renderer => _renderer;

    [SerializeField] protected CharacterStat _characterStat;
    public CharacterStat Stat => _characterStat;
    #endregion

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();

        HealthCompo = GetComponent<Health>();
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        CharController = GetComponent<CharacterController>();
        NavAgent = GetComponent<NavMeshAgent>();
        OutlineCompo = GetComponent<Outline>();
        ActionData = GetComponent<EntityActionData>();

        passiveData?.SetOwner(this);

        DamageCasterCompo.SetOwner(this);
        HealthCompo.SetHealth(_characterStat);
        HealthCompo.OnHit += HandleHit;
        HealthCompo.OnDied += HandleDie;

        _characterStat = Instantiate(_characterStat);
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
        if (passiveData == true)
            passiveData.Start();
    }

    protected virtual void Update()
    {
        if (passiveData == true)
            passiveData.Update();
    }

    protected abstract void HandleDie();

    public virtual void Attack()
    {
        DamageCasterCompo?.CastDamage();
    }

    public virtual void AoEAttack()
    {
        DamageCasterCompo?.CaseAoEDamage();
    }

    public virtual void RangeAttack()
    {

    }

    #region 패시브 함수

    /// <summary>
    /// 몇 대마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckAttackEventPassive(int curAttackCount)
=> passiveData.CheckAttackEventPassive(curAttackCount);

    /// <summary>
    /// 몇 초마다 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckSecondEventPassive(float curTime) => IsSecondEvent;

    /// <summary>
    /// 뒤치기 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckBackAttackEventPassive() 
        => IsAttackEvent;

    /// <summary>
    /// 주변의 적 수 비례 패시브 활성화 확인 여부
    /// </summary>
    /// <returns> 결과</returns>
    public bool CheckAroundEnemyCountEventPassive() => IsAttackEvent;
    #endregion

    public virtual void OnPassiveAttackEvent()
    {

    }
    public virtual void OnPassiveSecondEvent()
    {

    }
    public virtual void OnPassiveBackAttackEvent()
    {

    }
    public virtual void OnPassiveAroundEvent()
    {

    }
    #endregion


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
