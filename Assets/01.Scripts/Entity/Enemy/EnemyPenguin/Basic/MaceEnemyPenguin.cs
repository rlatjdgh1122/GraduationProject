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

        _curBuffItem = BuffList.Find(x => WaveManager.Instance.CurrentWaveCount == x.Wave);

        GiveBuff(_curBuffItem);
    }

    protected override void HandleDie()
    {
        base.HandleDie();

        TakeBuff(_curBuffItem);
    }

    private void GiveBuff(BuffItem item)
    {
        switch (item.BuffName)
        {
            case "Health":
                foreach (var solider in MyArmy.Soldiers)
                {
                    solider.StartEffect(item.BuffName);
                    _healCorou = StartCoroutine(Heal(item.BuffValue, solider));
                }
                break;

            case "AttackSpeed":
                foreach (var solider in MyArmy.Soldiers)
                {
                    solider.StartEffect(item.BuffName);
                    _defaultAttackSpeed = solider.attackSpeed;
                    solider.attackSpeed = item.BuffValue;
                }
                break;

            case "Damage":
                foreach (var solider in MyArmy.Soldiers)
                {
                    solider.StartEffect(item.BuffName);
                    solider.AddStat(item.BuffValue, StatType.Damage, StatMode.Increase);
                }
                break;
        }
    }

    private void TakeBuff(BuffItem item)
    {
        switch (item.BuffName)
        {
            case "Health":
                foreach (var solider in MyArmy.Soldiers)
                {
                    solider.StopEffect(item.BuffName);
                    StopCoroutine(_healCorou);
                }
                break;

            case "AttackSpeed":
                foreach (var solider in MyArmy.Soldiers)
                {
                    solider.StopEffect(item.BuffName);
                    solider.attackSpeed = _defaultAttackSpeed;
                }
                break;

            case "Damage":
                foreach (var solider in MyArmy.Soldiers)
                {
                    solider.StopEffect(item.BuffName);
                    solider.RemoveStat(item.BuffValue, StatType.Damage, StatMode.Increase);
                }
                break;

        }
    }

    private IEnumerator Heal(int value, Enemy target)
    {
        while (true)
        {
            yield return new WaitForSeconds(HealingDelay);
            target.HealthCompo.ApplyHeal(value);
        }

    }


}
