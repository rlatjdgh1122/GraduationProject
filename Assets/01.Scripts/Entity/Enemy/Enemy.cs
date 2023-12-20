using System;
using System.Linq;
using UnityEngine;

public abstract class Enemy : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 3f;
    public float attackSpeed = 1f;
    public float rotationSpeed = 2f;

    [SerializeField]
    [Range(0.1f, 6f)]
    protected float nexusDistance;

    public string playerLayer = "Player";

    #region �̺�Ʈ��
    public Action OnProvoked = null;
    #endregion

    public Penguin CurrentTarget;

    public bool IsMove = false;
    public bool IsDead = false;
    public bool IsTargetPlayerInside => CurrentTarget != null &&
                            Vector3.Distance(transform.position, CurrentTarget.transform.position) <= innerDistance;
    public bool CanAttack => CurrentTarget != null && 
                            Vector3.Distance(transform.position, CurrentTarget.transform.position) <= attackDistance;
    public bool ReachedNexus => 
                            Vector3.Distance(transform.position, NexusTarget.position) <= nexusDistance;
    public Transform NexusTarget => GameObject.Find("Nexus").transform;

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
        CurrentTarget = FindNearestPenguin("Player");
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void RangeAttack()
    {
        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation);
        arrow.Setting(this, this.GetType());
        arrow.Fire(_firePos.forward);
    }

    protected override void HandleDie()
    {
        IsDead = true;
    }

    public abstract void AnimationTrigger();

    public Penguin FindNearestPenguin(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();

        var nearestObject = objects
            .OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            })
            .FirstOrDefault();

        if (nearestObject != null)
        {
            Penguin penguinScript = nearestObject.GetComponent<Penguin>();

            if (penguinScript != null)
            {
                return CurrentTarget = penguinScript;
            }
            else
            {
                Debug.LogWarning("���� ����� ������Ʈ�� ��ũ��Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            return CurrentTarget = null;
        }

        // ������� �Դٸ� ������ �߻��߰ų� ����� ������Ʈ�� ã�� ���� ����̹Ƿ� null ��ȯ
        return null;
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
