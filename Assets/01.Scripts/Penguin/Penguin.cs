using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public abstract class Penguin : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 4.5f;
    public float attackSpeed = 1f;

    [Header("���ݻ��¼�����")]
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    [Header("���Ÿ����ݼ�����")]
    [SerializeField] protected Arrow _arrowPrefab;
    [SerializeField] protected Transform _firePos;

    public Enemy Target;

    public bool IsClickToMoving = false;
    public bool IsDead = false;

    protected int _lastAnimationBoolHash; //���������� ����� �ִϸ��̼� �ؽ�

    public bool IsInTargetRange => Vector3.Distance(transform.position, Target.transform.position) <= innerDistance;
    public bool IsAttackRange => Vector3.Distance(transform.position, Target.transform.position) <= attackDistance;

    [SerializeField] private InputReader _inputReader;
    public InputReader Input => _inputReader;

    protected override void Awake()
    {
        base.Awake();
        Target = FindNearestEnemy("Enemy");
        NavAgent.speed = moveSpeed;
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void RangeAttack()
    {
        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, Quaternion.identity);
        arrow.SetOwner(this);
        arrow.Fire(_firePos.forward);
    }

    public abstract void AnimationTrigger();

    public Enemy FindNearestEnemy(string tag)
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
            Enemy enemyScript = nearestObject.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                return Target = enemyScript;
            }
            else
            {
                Debug.LogWarning("���� ����� ������Ʈ�� Enemy ��ũ��Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            //Debug.LogWarning("����� ������Ʈ�� ã�� ���߽��ϴ�.");
            return Target = null;
        }

        // ������� �Դٸ� ������ �߻��߰ų� ����� ������Ʈ�� ã�� ���� ����̹Ƿ� null ��ȯ
        return null;
    }

    public void LookTarget()
    {
        if (Target != null)
        {
            Vector3 directionToTarget = Target.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
        }
    }

    protected override void HandleDie()
    {
        Debug.Log("���");
        IsDead = true;
    }
}
