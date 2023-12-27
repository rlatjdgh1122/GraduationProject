using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UIElements.Experimental;

public abstract class Entity : PoolableMono
{
    public int idx;
    [Header("Collision Info")]
    [SerializeField] protected LayerMask _whatIsWall;
    [SerializeField] protected LayerMask _whatIsHitable;
    [SerializeField] protected Transform _wallChecker;
    [SerializeField] protected float _wallCheckDistance;

    [Header("Knockback info")]
    [SerializeField] protected float _knockbackDuration;
    protected bool _isKnocked;

    [Header("Target info")]
    public Vector3 targetTrm;
    public float innerDistance = 4f;
    public float attackDistance = 1.5f;

    [Header("RangeAttack Info")]
    [SerializeField] protected Arrow _arrowPrefab;
    [SerializeField] protected Transform _firePos;

    #region ������Ʈ
    public ParticleSystem HitEffect { get; private set; }
    public ParticleSystem HealEffect { get; private set; }
    public Animator AnimatorCompo { get; private set; }
    public Health HealthCompo { get; private set; }
    public DamageCaster DamageCasterCompo { get; private set; }
    public CharacterController CharController { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }
    public ParticleSystem ClickParticle;
    public Outline OutlineCompo { get; private set; }

    [SerializeField] protected SkinnedMeshRenderer _renderer;
    public SkinnedMeshRenderer Renderer => _renderer;

    [SerializeField] protected CharacterStat _characterStat;
    public CharacterStat Stat => _characterStat;
    #endregion

    public UnityEvent<float> OnHealthBarChanged;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        Transform hitEffectTrm = transform?.Find("HitEffect");
        HitEffect = hitEffectTrm?.GetComponent<ParticleSystem>();
        Transform healEffectTrm = transform?.Find("HealEffect");
        HealEffect = healEffectTrm?.GetComponent<ParticleSystem>();

        HealthCompo = GetComponent<Health>();
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        CharController = GetComponent<CharacterController>();
        NavAgent = GetComponent<NavMeshAgent>();
        ClickParticle = GameObject.Find("ClickParticle").GetComponent<ParticleSystem>();
        OutlineCompo = GetComponent<Outline>();
        
        DamageCasterCompo.SetOwner(this, castByCloneSkill: false); //�ڽ��� ���Ȼ� �������� �־���.
        HealthCompo.SetOwner(_characterStat);
        HealthCompo.OnHit += HandleHit;
        HealthCompo.OnDied += HandleDie;

        _characterStat = Instantiate(_characterStat); //���������� ž��.
        _characterStat.SetOwner(this); //�ڱ⸦ ���ʷ� ����
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

    public void SetFirstPosition(Vector3 vec) // 정민교 추가
    {
        NavAgent.SetDestination(vec);
    }

    #region �̵� ����
    public void SetClickMovement()
    {
        RaycastHit hit;

        if (Physics.Raycast(GameManager.Instance.RayPosition(), out hit))
        {
            ArmySystem.Instace.SetArmyMovePostiton(hit.point, idx);

            ClickParticle.transform.position = hit.point + new Vector3(0, 0.1f, 0);
            ClickParticle.Play();
        }
    }

    public void SetTarget(Vector3 _target)
    {
        if (NavAgent.isActiveAndEnabled)
        {
            targetTrm = _target;
            MoveToTarget();
        }
    }

    public void MoveToTarget()
    {
        NavAgent.ResetPath();
        NavAgent.SetDestination(targetTrm);
    }

    public void StopImmediately()
    {
        if (NavAgent.isActiveAndEnabled)
            NavAgent.isStopped = true;
    }
    #endregion
}