using System.Collections;
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

    #region ������Ʈ
    public Animator AnimatorCompo { get; private set; }
    //public Health HealthCompo { get; private set; }
    //public DamageCaster DamageCasterCompo { get; private set; }
    public CharacterController CharController { get; private set; }
    public NavMeshAgent NavAgent { get; private set; }

    //[SerializeField] protected CharacterStat _characterStat;
    //public CharacterStat Stat => _characterStat;
    #endregion

    public int FacingDirection { get; private set; } = 1; //�������� 1, ������ -1
    protected bool _facingRight = true;
    public UnityEvent<float> OnHealthBarChanged;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        //HealthCompo = GetComponent<Health>();
        //DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        CharController = GetComponent<CharacterController>();
        NavAgent = GetComponent<NavMeshAgent>();

        //DamageCasterCompo.SetOwner(this, castByCloneSkill: false); //�ڽ��� ���Ȼ� �������� �־���.
        //HealthCompo.SetOwner(this);

        //HealthCompo.OnKnockBack += HandleKnockback;
        //HealthCompo.OnHit += HandleHit;
        //HealthCompo.OnDied += HandleDie;
        //OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth()); //�ִ�ġ�� UI����.

        //_characterStat = Instantiate(_characterStat); //���������� ž��.
        //_characterStat.SetOwner(this); //�ڱ⸦ ���ʷ� ����
    }

    private void OnDestroy()
    {
        //HealthCompo.OnKnockBack -= HandleKnockback;
        //HealthCompo.OnHit -= HandleHit;
        //HealthCompo.OnDied -= HandleDie;
    }

    protected virtual void HandleHit()
    {
        //UI����
        //OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth());
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }


    //protected virtual void HandleKnockback(Vector2 direction)
    //{
    //    StartCoroutine(HitKnockback(direction));
    //}

    protected abstract void HandleDie();

    //protected virtual IEnumerator HitKnockback(Vector2 direction)
    //{
    //    _isKnocked = true;
    //    RigidbodyCompo.velocity = direction;
    //    yield return new WaitForSeconds(_knockbackDuration);
    //    _isKnocked = false;
    //}

    #region �ӵ� ����
    public void SetMovement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.point);
            NavAgent.SetDestination(hit.point);
        }
    }

    public void StopImmediately()
    {
        NavAgent.isStopped = true;
    }
    #endregion

    #region ȸ�� ����
    public void SetRotation(Vector3 rot)
    {
        transform.Rotate(rot);
    }
    #endregion

    #region �浹 ����
    public virtual bool IsWallDetected() =>
        Physics.Raycast(_wallChecker.position, Vector3.forward * FacingDirection,
                            _wallCheckDistance, _whatIsWall);
    #endregion
}
