using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(FeedbackController))]
public abstract class Entity : Target
{
    public float innerDistance = 4f;
    public float attackDistance = 1.5f;

    [SerializeField] protected LayerMask TargetLayer;

    #region Components 
    public Animator AnimatorCompo { get; protected set; }
    public NavMeshAgent NavAgent { get; protected set; }
    public EntityActionData ActionData { get; private set; }
    public Outline OutlineCompo { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm?.GetComponent<Animator>(); //이건일단 모르겠어서 ?. 이렇게 해놈
        NavAgent = transform?.GetComponent<NavMeshAgent>();
        OutlineCompo = transform?.GetComponent<Outline>(); //이것도 따로 컴포넌트로 빼야함
        ActionData = GetComponent<EntityActionData>();
    }

    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    protected override void HandleHit()
    {

    }

    protected override void HandleDie()
    {

    }
    public void Provoke<T>(int provokeCount, float duration, float radius) where T : Target
    {
        T target = null;

        var colliders = new Collider[provokeCount];
        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, TargetLayer);

    }

    #region 움직임 관리
    public void MoveToPosition(Vector3 pos)
    {
        if (NavAgent.isActiveAndEnabled)
        {
            NavAgent?.ResetPath();
            NavAgent?.SetDestination(pos);
        }
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
            }

        }
    }
    #endregion
    public virtual T ReturnGenericStat<T>() where T : BaseStat
    {
        if (_characterStat is T)
        {
            return _characterStat as T;
        }

        Debug.LogError("Not T type");
        return null;
    }
}
