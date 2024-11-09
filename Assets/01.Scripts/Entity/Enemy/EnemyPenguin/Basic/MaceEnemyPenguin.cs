using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceEnemyPenguin : EnemyBasicPenguin
{
    public List<BuffItem> BuffList = new();
    public float HealingDelay = 10f;

    [System.Serializable]
    public class BuffItem
    {
        public int Wave;
        public string BuffName;
        public int BuffValue;
    }

    private Coroutine _healCorou = null;
    private float _defaultAttackSpeed = 0f;

    private BuffItem _curBuffItem = null;

    protected override void Start()
    {
        base.Start();
    }

    public override void Init()
    {
        base.Init();

        _curBuffItem = BuffList.Find(x => WaveManager.Instance.CurrentWaveCount == x.Wave);

        CoroutineUtil.CallWaitForSeconds(0.5f,
            () =>
            {
                foreach (var solider in MyArmy.Soldiers)
                    GiveBuff(solider);
            });

    }

    protected override void HandleDie()
    {
        base.HandleDie();

        foreach (var solider in MyArmy.Soldiers)
            TakeBuff(solider);
    }

    private void GiveBuff(Enemy solider)
    {
        solider.OnDied += TakeBuff;

        switch (_curBuffItem.BuffName)
        {
            case "Health":
                solider.StartEffect(_curBuffItem.BuffName);
                _healCorou = StartCoroutine(Heal(_curBuffItem.BuffValue, solider));
                break;

            case "AttackSpeed":
                solider.StartEffect(_curBuffItem.BuffName);
                _defaultAttackSpeed = solider.attackSpeed;
                solider.attackSpeed = _curBuffItem.BuffValue;
                break;

            case "Damage":
                solider.StartEffect(_curBuffItem.BuffName);
                solider.AddStat(_curBuffItem.BuffValue, StatType.Damage, StatMode.Increase);
                break;
        }
    }

    private void TakeBuff(Enemy solider)
    {
        solider.OnDied -= TakeBuff;

        switch (_curBuffItem.BuffName)
        {
            case "Health":
                solider.StopEffect(_curBuffItem.BuffName);
                StopCoroutine(_healCorou);
                break;

            case "AttackSpeed":
                solider.StopEffect(_curBuffItem.BuffName);
                solider.attackSpeed = _defaultAttackSpeed;
                break;

            case "Damage":
                solider.StopEffect(_curBuffItem.BuffName);
                solider.RemoveStat(_curBuffItem.BuffValue, StatType.Damage, StatMode.Increase);
                break;

        }

    }

    private IEnumerator Heal(int value, Enemy target)
    {
        while (true)
        {
            yield return new WaitForSeconds(HealingDelay);

            if (target.enabled)
                target.HealthCompo.ApplyHeal(value);
        }

    }


}
