using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Penguin : Entity
{
    public float moveSpeed = 4.5f;
    public float attackSpeed = 1f;
    public int maxDetectedCount;
    public float provokeRange = 25f;

    #region components
    public EntityAttackData AttackCompo { get; private set; }
    #endregion

    public Enemy CurrentTarget;

    public bool IsDead = false;
    public bool IsInnerTargetRange => CurrentTarget != null && Vector3.Distance(MousePos, CurrentTarget.transform.position) <= innerDistance;
    public bool IsInnerMeleeRange => CurrentTarget != null && Vector3.Distance(transform.position, CurrentTarget.transform.position) <= attackDistance;

    public Army owner;

    public Army Owner => owner;

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

        AttackCompo = GetComponent<EntityAttackData>();
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
        ArmyManager.Instance.Remove(Owner.Legion, this);
        IsDead = true;
    }

    public void AddStat(int value, StatType type, StatMode mode)
    {
        Stat.AddStat(value, type, mode);
    }
    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        Stat.RemoveStat(value, type, mode);
    }

    public IEnumerator AddStatCorou(float time, int value, StatType type, StatMode mode)
    {
        yield return new WaitForSeconds(time);
        Stat.AddStat(value, type, mode);
    }

    public override void Init()
    {
        owner = null;
    }
}
