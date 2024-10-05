using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SingingStateEnum : byte
{
    Idle,
    Singing,

}

public class EnemySingingPenguin : Enemy
{
    public StatType BuffStatType = StatType.None;
    public int BuffValue = 0;
    public int TargetCount = 3;

    private List<Enemy> _enemies = new List<Enemy>();

    protected override void Awake()
    {
        base.Awake();

        FindTargets();
    }

    public override void StateInit()
    {
        //���⼭ �����Ҷ� �������� ���·� ��ȯ������
        //StateMachine.Init();
    }


    private void FindTargets()
    {
        int count = Mathf.Clamp(TargetCount, 0, GameManager.Instance.GetCurrentEnemyCount());
        Enemy[] enemyPenguins = FindObjectsOfType<Enemy>();

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = enemyPenguins[i];

            //�ߺ��� Ÿ���� ��� ��Ƽ��
            if (_enemies.Contains(enemy)) continue;

            enemy.AddStat(BuffValue, BuffStatType, StatMode.Increase);
            enemy.OnDied += OnTargetDeadEvent;

            _enemies.Add(enemy);
        }


    }

    public void OnTargetDeadEvent(Enemy enemy)
    {
        //����� ��� ���� �����
        enemy.RemoveStat(BuffValue, BuffStatType, StatMode.Increase);
        _enemies.Remove(enemy);

        //�������� ��ٸ��� �������� �� ���� Ÿ�� ����
        CoroutineUtil.CallWaitForOneFrame(() => 
        {
            enemy.OnDied -= OnTargetDeadEvent;
            FindTargets();
        });
    }


}
