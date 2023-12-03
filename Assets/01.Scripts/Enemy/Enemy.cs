using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 12f;
    public float attackDelay = 0.5f;
    public float attackTime = 0.5f;

    public Transform NexusTarget;

    protected bool isDead = false;

    public bool IsTargetPlayerInside => Vector3.Distance(transform.position, target.position) <= innerDistance;
    public bool IsAttackable => Vector3.Distance(transform.position, target.position) <= attackDistance;
    public bool ReachedNexus => Vector3.Distance(transform.position, NexusTarget.position) <= attackDistance;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Attack()
    {
        base.Attack();
    }

    protected override void HandleDie()
    {
        //Á×¾úÀ»¶§ ¹¹ÇØÁÙÁö
        isDead = true;
        Debug.Log("Áê±Ý");
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
}
