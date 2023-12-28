using System.Linq;
using UnityEngine;
using  Define.Algorithem;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.AI;

public class Penguin : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 4.5f;
    public float attackSpeed = 1f;
    public int maxDetectedCount;
    public float provokeRange = 25f;

    public Enemy CurrentTarget;

    public bool IsClickToMoving = false;
    public bool IsDead = false;
    public bool IsInnerTargetRange => CurrentTarget != null && Vector3.Distance(Algorithm.AlignmentRule.GetArmyCenterPostion(owner), CurrentTarget.transform.position) <= innerDistance;
    public bool IsInnerMeleeRange => CurrentTarget != null && Vector3.Distance(transform.position, CurrentTarget.transform.position) <= attackDistance;

    public Army owner;

    [SerializeField] private InputReader _inputReader;
    public InputReader Input => _inputReader;

    private void OnEnable()
    {
        WaveManager.Instance.OnIceArrivedEvent += FindFirstNearestEnemy;
    }

    private void OnDisable()
    {
        WaveManager.Instance.OnIceArrivedEvent -= FindFirstNearestEnemy;
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

    public void FindFirstNearestEnemy()
    {
        CurrentTarget = FindNearestEnemy().FirstOrDefault();
    }

    public List<Enemy> FindNearestEnemy(int count = 1)
    {
        Enemy[] objects = FindObjectsOfType<Enemy>().Where(e => e.enabled).ToArray();

        var nearbyEnemies = objects
            .Where(obj => Vector3.Distance(transform.position, obj.transform.position) <= provokeRange)
            .OrderBy(obj => Vector3.Distance(transform.position, obj.transform.position))
            .Take(count)
            .ToList();

        return nearbyEnemies;
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