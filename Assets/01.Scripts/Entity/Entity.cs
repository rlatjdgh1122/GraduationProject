using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : PoolableMono
{
    [Header("Target info")]
    public float innerDistance = 4f;
    public float attackDistance = 1.5f;

    [Header("RangeAttack Info")]
    [SerializeField] protected Arrow _arrowPrefab;
    [SerializeField] protected Transform _firePos;

    [Header("MopGeneral Info")]
    [SerializeField] public int AoEAttackCount = 3;


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
