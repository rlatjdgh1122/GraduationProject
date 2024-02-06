using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : PoolableMono
{
    public int idx;

    [Header("Target info")]
    public Vector3 targetTrm;
    public float innerDistance = 4f;
    public float attackDistance = 1.5f;

    [Header("RangeAttack Info")]
    [SerializeField] protected Arrow _arrowPrefab;
    [SerializeField] protected Transform _firePos;

    private Vector3 _seatPos = Vector3.zero; //군단에서 배치된 자리 OK?
    public Vector3 SeatPos
    {
        get => _seatPos;
        set { _seatPos = value; }
    }

    #region Components
    public Animator AnimatorCompo { get; private set; }
    public Health HealthCompo { get; private set; }
    public DamageCaster DamageCasterCompo { get; private set; }
    public CharacterController CharController { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
    public EntityActionData ActionData { get; private set; }
    public ParticleSystem ClickParticle;
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
        ClickParticle = GameObject.Find("ClickParticle").GetComponent<ParticleSystem>();
        OutlineCompo = GetComponent<Outline>();
        ActionData = GetComponent<EntityActionData>();

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

    }

    protected virtual void Update()
    {

    }

    protected abstract void HandleDie();

    public virtual void Attack()
    {
        DamageCasterCompo?.CastDamage();
    }

    public virtual void RangeAttack()
    {

    }

    #region �̵� ����
    public void SetClickMovement()
    {

        Debug.Log("이 함수 사용하냐???");
        RaycastHit hit;

        if (Physics.Raycast(GameManager.Instance.RayPosition(), out hit))
        {
            //ArmySystem.Instace.SetArmyMovePostiton(hit.point, idx);

            ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
            ClickParticle.Play();
        }
    }

    public void MoveToMySeat(Vector3 mousePos) //싸울때말고 군단 위치로
    {
        if (NavAgent.isActiveAndEnabled)
        {
            //targetTrm = target;
            MoveToTarget(mousePos + SeatPos);
        }
    }

    public void SetTarget(Vector3 mousePos)
    {
        if (NavAgent.isActiveAndEnabled)
        {
            //targetTrm = target;
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
