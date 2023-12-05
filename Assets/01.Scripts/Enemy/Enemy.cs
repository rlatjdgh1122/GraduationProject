using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 3f;
    public float attackSpeed = 1f;

    public Penguin Target;

    public Transform NexusTarget => GameObject.Find("Nexus").transform;

    public bool IsDead = false;

    public bool IsTargetPlayerInside => Target != null && Vector3.Distance(transform.position, Target.transform.position) <= innerDistance;
    public bool IsAttackable => Target != null && Vector3.Distance(transform.position, Target.transform.position) <= attackDistance;
    public bool ReachedNexus => Vector3.Distance(transform.position, NexusTarget.position) <= attackDistance;

    protected override void Awake()
    {
        base.Awake();
        Target = FindNearestPenguin("Player");
        NavAgent.speed = moveSpeed;
    }

    public override void Attack()
    {
        base.Attack();
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
                Debug.LogWarning("���� ����� ������Ʈ�� Enemy ��ũ��Ʈ�� �����ϴ�.");
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
        NavAgent.ResetPath();
        NavAgent.SetDestination(NexusTarget.position);
    }

    public void LookAtNexus()
    {
        transform.LookAt(NexusTarget);
    }

    public void LookTarget()
    {
        //transform.Rotate(targetTrm);
        if (Target != null)
            transform.LookAt(Target.transform.position);
    }
}
