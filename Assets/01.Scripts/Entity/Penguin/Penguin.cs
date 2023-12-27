using System.Linq;
using UnityEngine;
using  Define.Algorithem;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class Penguin : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 4.5f;
    public float attackSpeed = 1f;
    public int maxDetectedCount;

    public Enemy CurrentTarget;

    public bool IsClickToMoving = false;
    public bool IsDead = false;
    public bool IsInnerTargetRange => CurrentTarget != null && Vector3.Distance(Algorithm.AlignmentRule.GetArmyCenterPostion(owner), CurrentTarget.transform.position) <= innerDistance;
    public bool IsInnerMeleeRange => CurrentTarget != null && Vector3.Distance(transform.position, CurrentTarget.transform.position) <= attackDistance;

    public List<Enemy> nearestEnemy;
    public Army owner;

    [SerializeField] private InputReader _inputReader;
    public InputReader Input => _inputReader;

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

    public override void RangeAttack()
    {
        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation);
        arrow.Setting(this, DamageCasterCompo.TargetLayer);
        arrow.Fire(_firePos.forward);
    }

    public void SetOwner(Army army)
    {
        owner = army;
    }

    public virtual void AnimationTrigger()
    {

    }

    public void FindEnemy()
    {
        FindNearestEnemy(maxDetectedCount);
    }

    public List<Enemy> FindNearestEnemy(int maxCount)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

        List<Enemy> enemies = objects
            .Where(obj => obj != null && GameManager.Instance.GetCurrentEnemyCount() > 0)
            .Select(obj => obj.GetComponent<Enemy>())
            .Where(enemyScript => enemyScript != null && Vector3.Distance(transform.position, enemyScript.transform.position) <= 25f)
            .OrderBy(enemyScript => Vector3.Distance(transform.position, enemyScript.transform.position))
            .Take(maxCount)  // OrderBy ������ Take�� ����
            .ToList();

        if (enemies.Count > 0)
        {
            // ���� ����� ���� CurrentTarget���� ����
            CurrentTarget = enemies[0];
            return enemies;
        }
        else
        {
            Debug.LogWarning("���� ����� ������Ʈ�� Enemy ��ũ��Ʈ�� ���ų� ���� ���� �����ϴ�.");
            CurrentTarget = null;
            return null;
        }
    }

    public void LookTarget()
    {
        if (CurrentTarget != null)
        {
            Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
        }
    }

    protected override void HandleDie()
    {
        ArmySystem.Instace.Remove(owner.Legion, this);
        IsDead = true;
    }
}
