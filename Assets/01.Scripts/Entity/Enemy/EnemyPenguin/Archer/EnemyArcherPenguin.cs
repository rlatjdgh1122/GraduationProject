using System;
using UnityEngine;

public class EnemyArcherPenguin : Enemy
{

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Init(EnemyStateType.Idle);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Init()
    {
        base.Init();
        Debug.Log($"{name} : »ý¼º");
        StateMachine.Init(EnemyStateType.Idle);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}