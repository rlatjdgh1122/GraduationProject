using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyGorillaStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Reached,
    MustChase,
    Provoked,
    ChestHit
}

public class EnemyGorilla : Enemy
{
    public Skill VigilanceSkill { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        VigilanceSkill = transform.Find("SkillManager").GetComponent<Skill>();
    }

    protected override void Start()
    {
        base.Start();

        VigilanceSkill.SetOwner(this);

        StateMachine.Init(EnemyStateType.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public void OnSkillEvent()
    {
        StateMachine.ChangeState(EnemyStateType.ChestHit);
    }

    public override void Init()
    {
        base.Init();

        StateMachine.Init(EnemyStateType.Idle);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
