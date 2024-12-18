using System;
using UnityEngine;

public enum EnemyPenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Reached,
    MustChase,
}

public class EnemyBasicPenguin : Enemy
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Init(EnemyStateType.Idle);

/*        foreach (var enemy in MyArmy.Soldiers)
        {
            enemy.StartEffect("Health");
        }*/
    }

   
    protected override void OnDestroy()
    {
        base.OnDestroy();

    }

    protected override void Update()
    {
        StateMachine!.CurrentState!.UpdateState();
    }

  public override void Init()
    {
        base.Init();
        StateMachine.Init(EnemyStateType.Idle);
    }

    protected override void HandleDie()
    {
        base.HandleDie();

        foreach (var enemy in MyArmy.Soldiers)
        {
            enemy.StopEffect("Health");
        }
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
