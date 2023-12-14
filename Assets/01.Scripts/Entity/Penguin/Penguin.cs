using System.Linq;
using UnityEngine;
using  Define.Algorithem;
public abstract class Penguin : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 4.5f;
    public float attackSpeed = 1f;

    [Header("공격상태설정값")]
    public float attackCooldown;
    public float lastTimeAttacked = 0f;

    public Enemy Target;

    public bool IsClickToMoving = false;
    public bool IsDead = false;

    protected int _lastAnimationBoolHash; //마지막으로 재생된 애니메이션 해시

    public bool IsInTargetRange => Target != null && Vector3.Distance(Algorithm.AlignmentRule.GetArmyCenterPostion(owner), Target.transform.position) <= innerDistance;
    public bool IsAttackRange => Target != null && Vector3.Distance(transform.position, Target.transform.position) <= attackDistance;

    [SerializeField] private InputReader _inputReader;
    public InputReader Input => _inputReader;

    private float _distance;
    public Enemy nearestEnemy;

    public Army owner;

    private void OnEnable()
    {
        WaveManager.Instance.OnIceArrivedEvent += FindEnemy;
    }

    private void OnDisable()
    {
        WaveManager.Instance.OnIceArrivedEvent -= FindEnemy;
    }

    protected override void Awake()
    {
        base.Awake();
        NavAgent.speed = moveSpeed;
    }

    public override void Attack()
    {
        base.Attack();
    }

    public void SetOwner(Army army)
    {
        owner = army;
    }

    public abstract void AnimationTrigger();

    public void FindEnemy()
    {
        FindNearestEnemy("Enemy");
    }

    public Enemy FindNearestEnemy(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag).ToList();

        var nearestObject = objects
            .OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            })
            .FirstOrDefault();

        if (nearestObject != null && GameManager.Instance.GetCurrentEnemyCount() > 0)
        {
            Enemy enemyScript = nearestObject.GetComponent<Enemy>();

            if (enemyScript != null)
            {
                return Target = enemyScript;
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
        Debug.Log("쥬금");
        ArmySystem.Instace.Remove(0, this);
        IsDead = true;
    }
}
