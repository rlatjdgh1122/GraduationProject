using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    [SerializeField] protected LayerMask _whatIsWall;
    [SerializeField] protected Transform _wallChecker;
    [SerializeField] protected float _wallCheckDistance;

    [Header("Knockback info")]
    [SerializeField] protected float _knockbackDuration;
    protected bool _isKnocked;

    [Header("Target info")]
    public Vector3 target;
    public Transform enemy;
    public float innerDistance = 4f;
    public float attackDistance = 1.5f;

    #region 컴포넌트
    public Animator AnimatorCompo { get; private set; }
    public Health HealthCompo { get; private set; }
    public DamageCaster DamageCasterCompo { get; private set; }
    public CharacterController CharController { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }

    [SerializeField] protected CharacterStat _characterStat;
    public CharacterStat Stat => _characterStat;
    #endregion

    public int FacingDirection { get; private set; } = 1; //오른쪽이 1, 왼쪽이 -1
    protected bool _facingRight = true;
    public UnityEvent<float> OnHealthBarChanged;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        HealthCompo = GetComponent<Health>();
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        CharController = GetComponent<CharacterController>();
        NavAgent = GetComponent<NavMeshAgent>();

        DamageCasterCompo.SetOwner(this, castByCloneSkill: false); //자신의 스탯상 데미지를 넣어줌.
        HealthCompo.SetOwner(this);

        //HealthCompo.OnKnockBack += HandleKnockback;
        HealthCompo.OnHit += HandleHit;
        HealthCompo.OnDied += HandleDie;
        //OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth()); //최대치로 UI변경.

        _characterStat = Instantiate(_characterStat); //복제본으로 탑재.
        _characterStat.SetOwner(this); //자기를 오너로 설정
    }

    private void OnDestroy()
    {
        //HealthCompo.OnKnockBack -= HandleKnockback;
        HealthCompo.OnHit -= HandleHit;
        HealthCompo.OnDied -= HandleDie;
    }

    protected virtual void HandleHit()
    {
        //UI갱신
        //OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth());
    }

    public void SetTarget(Vector3 _target)
    {
        target = _target;
        MoveToTarget();
    }
    
    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected abstract void HandleDie();

    public Transform FindNearestObjectByTag(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();

        var neareastObject = objects
            .OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            })
        .FirstOrDefault();

        return enemy = neareastObject.transform;
    }

    public virtual void Attack()
    {
        DamageCasterCompo?.CastDamage();
    }

    #region 이동 관련
    public void SetMovement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit))
        {
            NavAgent.ResetPath();
            SetTarget(hit.point);   
        }
    }

    public void MoveToTarget()
    {
        NavAgent.ResetPath();
        NavAgent.SetDestination(target);
    }

    public void StopImmediately()
    {
        NavAgent.isStopped = true;
    }
    #endregion

    #region 회전 관련
    public void SetRotation(Vector3 rot)
    {
        transform.Rotate(rot);
    }
    #endregion

    #region 충돌 관련
    public virtual bool IsWallDetected() =>
        Physics.Raycast(_wallChecker.position, Vector3.forward * FacingDirection,
                            _wallCheckDistance, _whatIsWall);
    #endregion
}
