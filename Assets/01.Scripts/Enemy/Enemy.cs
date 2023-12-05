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
        Debug.Log("쥬금");
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
                Debug.LogWarning("가장 가까운 오브젝트에 Enemy 스크립트가 없습니다.");
            }
        }
        else
        {
            return Target = null;
        }

        // 여기까지 왔다면 오류가 발생했거나 가까운 오브젝트를 찾지 못한 경우이므로 null 반환
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
