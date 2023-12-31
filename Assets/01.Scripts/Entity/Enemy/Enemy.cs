using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 3f;
    public float attackSpeed = 1f;
    public float rotationSpeed = 2f;
    [SerializeField]
    [Range(0.1f, 6f)]
    protected float nexusDistance;

    [Header("Action & Events")]
    public Action OnProvoked = null;
    public UnityEvent OnProvokedEvent;

    public Penguin CurrentTarget;
    public Transform NexusTarget => GameObject.Find("Nexus").transform;

    public bool IsMove = false;
    public bool IsDead = false;
    public bool IsProvoked = false;

    public bool IsTargetPlayerInside => CurrentTarget != null &&
                            Vector3.Distance(transform.position, CurrentTarget.transform.position) <= innerDistance;
    public bool CanAttack => CurrentTarget != null && 
                            Vector3.Distance(transform.position, CurrentTarget.transform.position) <= attackDistance;
    public bool IsReachedNexus => 
                            Vector3.Distance(transform.position, NexusTarget.position) <= nexusDistance;

    protected override void Awake()
    {
        base.Awake();
        NavAgent.speed = moveSpeed;
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnIceArrivedEvent += SetTarget;
    }

    private void OnDestroy()
    {
        WaveManager.Instance.OnIceArrivedEvent -= SetTarget;
    }

    private void SetTarget()
    {
        CurrentTarget = FindNearestPenguin<Penguin>();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void RangeAttack()
    {
        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation);
        arrow.Setting(this, DamageCasterCompo.TargetLayer);
        arrow.Fire(_firePos.forward);
    }

    protected override void HandleDie()
    {
        IsDead = true;
    }

    public virtual void AnimationTrigger()
    {
        
    }

    public T FindNearestPenguin<T>() where T : Penguin //OnProvoked bool�� ����
    {
        var components = FindObjectsOfType<T>().Where(p => p.enabled);

        var nearestObject = components
            .OrderBy(obj => Vector3.Distance(obj.transform.position, transform.position))
            .FirstOrDefault();

        if (nearestObject != null)
            return nearestObject;

        return default;
    }

    public void MoveToNexus()
    {
        NavAgent.ResetPath();
        NavAgent.SetDestination(NexusTarget.position);
    }

    public void LookAtNexus()
    {
        if (NexusTarget != null)
        {
            Vector3 directionToTarget = NexusTarget.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void LookTarget()
    {
        if (CurrentTarget != null)
        {
            Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
