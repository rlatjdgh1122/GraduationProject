using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;

public abstract class Entity : PoolableMono
{
    public int idx;
    [Header("Collision Info")]
    [SerializeField] protected LayerMask _whatIsWall;
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

    public ParticleSystem HitEffect;

    #region ������Ʈ
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

    public int FacingDirection { get; private set; } = 1; //�������� 1, ������ -1
    protected bool _facingRight = true;
    public UnityEvent<float> OnHealthBarChanged;

    private MaterialPropertyBlock mpb;

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
        
        DamageCasterCompo.SetOwner(this, castByCloneSkill: false); //�ڽ��� ���Ȼ� �������� �־���.
        HealthCompo.SetOwner(_characterStat);


        //HealthCompo.OnKnockBack += HandleKnockback;
        HealthCompo.OnHit += HandleHit;
        HealthCompo.OnDied += HandleDie;
        //OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth()); //�ִ�ġ�� UI����.

        _characterStat = Instantiate(_characterStat); //���������� ž��.
        _characterStat.SetOwner(this); //�ڱ⸦ ���ʷ� ����
    }

    private void OnDestroy()
    {
        //HealthCompo.OnKnockBack -= HandleKnockback;
        HealthCompo.OnHit -= HandleHit;
        HealthCompo.OnDied -= HandleDie;
    }

    protected virtual void HandleHit()
    {
        //mpb = new MaterialPropertyBlock();

        //Debug.Log("맞음");
        //mpb.SetColor("_EmissionColor", Color.white);
        //_renderer.SetPropertyBlock(mpb);
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
        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation);
        arrow.SetOwner(this, "Enemy");
        arrow.Fire(_firePos.forward);
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

    #region ȸ�� ����
    //public void LookTarget()
    //{
    //    //transform.Rotate(targetTrm);
    //    transform.LookAt(targetTrm);
    //}
    #endregion

    #region �浹 ����
    public virtual bool IsWallDetected() =>
        Physics.Raycast(_wallChecker.position, Vector3.forward * FacingDirection,
                            _wallCheckDistance, _whatIsWall);
    #endregion
}
