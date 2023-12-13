using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 3f;
    public float attackSpeed = 1f;
    public float rotationSpeed = 2f;

    [SerializeField]
    [Range(0.1f, 6f)]
    protected float nexusDistance;

    public Penguin Target;

    public bool IsMove = false;

    public Transform NexusTarget => GameObject.Find("Nexus").transform;

    public bool IsDead = false;

    public bool IsTargetPlayerInside => Target != null && Vector3.Distance(transform.position, Target.transform.position) <= innerDistance;
    public bool IsAttackable => Target != null && Vector3.Distance(transform.position, Target.transform.position) <= attackDistance;
    public bool ReachedNexus => Vector3.Distance(transform.position, NexusTarget.position) <= nexusDistance;

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
        Target = FindNearestPenguin("Player");
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void RangeAttack()
    {
        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation);
        arrow.SetOwner(this, "Player");
        arrow.Fire(_firePos.forward);
    }

    protected override void HandleDie()
    {
        IsDead = true;
        Debug.Log("���");
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
                return Target = penguinScript;
            }
            else
            {
                Debug.LogWarning("���� ����� ������Ʈ�� ��ũ��Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            return Target = null;
        }

        // ������� �Դٸ� ������ �߻��߰ų� ����� ������Ʈ�� ã�� ���� ����̹Ƿ� null ��ȯ
        return null;
    }

    public void MoveToNexus()
    {
        if (NavAgent.isActiveAndEnabled)
        {
            NavAgent.ResetPath();
            NavAgent.SetDestination(NexusTarget.position);
        }
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
        if (Target != null)
        {
            Vector3 directionToTarget = Target.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
