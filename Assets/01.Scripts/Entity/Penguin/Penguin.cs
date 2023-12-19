using System.Linq;
using UnityEngine;
using  Define.Algorithem;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public abstract class Penguin : Entity
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
        arrow.Setting(this, this.GetType());
        arrow.Fire(_firePos.forward);
    }

    public void SetOwner(Army army)
    {
        owner = army;
    }

    public abstract void AnimationTrigger();

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
            .Where(enemyScript => enemyScript != null)
            .OrderBy(enemyScript => Vector3.Distance(transform.position, enemyScript.transform.position))
            .ToList();

        if (enemies.Count > 0)
        {
            // 가장 가까운 적을 CurrentTarget으로 설정
            CurrentTarget = enemies[0];
            return enemies.Take(maxCount).ToList();
        }
        else
        {
            Debug.LogWarning("가장 가까운 오브젝트에 Enemy 스크립트가 없거나 현재 적이 없습니다.");
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
