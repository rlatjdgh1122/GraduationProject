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
        //여기서 시작할때 실행해줄 상태로 변환시켜줌
        //StateMachine.Init();
    }


    private void FindTargets()
    {
        int count = Mathf.Clamp(TargetCount, 0, GameManager.Instance.GetCurrentEnemyCount());
        Enemy[] enemyPenguins = FindObjectsOfType<Enemy>();

        for (int i = 0; i < count; i++)
        {
            Enemy enemy = enemyPenguins[i];

            //중복된 타겟일 경우 컨티뉴
            if (_enemies.Contains(enemy)) continue;

            enemy.AddStat(BuffValue, BuffStatType, StatMode.Increase);
            enemy.OnDied += OnTargetDeadEvent;

            _enemies.Add(enemy);
        }


    }

    public void OnTargetDeadEvent(Enemy enemy)
    {
        //사망할 경우 스탯 지우고
        enemy.RemoveStat(BuffValue, BuffStatType, StatMode.Increase);
        _enemies.Remove(enemy);

        //한프레임 기다리고 구독해제 및 다음 타겟 지정
        CoroutineUtil.CallWaitForOneFrame(() => 
        {
            enemy.OnDied -= OnTargetDeadEvent;
            FindTargets();
        });
    }


}
