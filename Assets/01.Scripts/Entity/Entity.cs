using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : PoolableMono
{

    [SerializeField] protected BaseStat _characterStat;
    public BaseStat Stat => _characterStat;
    public Entity CurrentTarget;
    public bool IsDead = false;

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
    #region Components
    public Health HealthCompo { get; private set; }
    public Animator AnimatorCompo { get; protected set; }
    public NavMeshAgent NavAgent { get; protected set; }
    public EntityActionData ActionData { get; private set; }
    public Outline OutlineCompo { get; private set; }

    #endregion

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm?.GetComponent<Animator>(); //이건일단 모르겠어서 ?. 이렇게 해놈
        HealthCompo = transform?.GetComponent<Health>();
        NavAgent = transform?.GetComponent<NavMeshAgent>();
        OutlineCompo = transform?.GetComponent<Outline>(); //이것도 따로 컴포넌트로 빼야함
        ActionData = GetComponent<EntityActionData>();

        HealthCompo?.SetHealth(_characterStat);
        _characterStat = Instantiate(_characterStat);


        if (HealthCompo != null)
        {
            HealthCompo.OnHit += HandleHit;
            HealthCompo.OnDied += HandleDie;
        }
    }

    private void OnDestroy()
    {
        if (HealthCompo != null)
        {
            HealthCompo.OnHit -= HandleHit;
            HealthCompo.OnDied -= HandleDie;
        }
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
    public void MoveToPosition(Vector3 pos)
    {
        NavAgent?.ResetPath();
        NavAgent?.SetDestination(pos);
    }
    public void MoveToCurrentTarget()
    {
        if (NavAgent.isActiveAndEnabled)
        {
            NavAgent.ResetPath();
            NavAgent.SetDestination(CurrentTarget.transform.position);
        }
    }

    public void StopImmediately()
    {
        if (NavAgent != null)
        {
            if (NavAgent.isActiveAndEnabled)
            {
                NavAgent.isStopped = true;
                NavAgent.velocity = Vector3.zero;
            }
        }
    }
    public void StartImmediately()
    {
        if (NavAgent != null)
        {
            if (NavAgent.isActiveAndEnabled)
            {
                NavAgent.isStopped = false;
                NavAgent.velocity = Vector3.one * .2f;
            }

        }
    }
    #endregion
}
