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
    [SerializeField] private int _vigilanceMaxLevel = 5;

    public Skill VigilanceSkill { get; private set; }

    public int CurrentLevel { get; private set; } = 0;

    protected override void Awake()
    {
        base.Awake();

        VigilanceSkill = transform.Find("SkillEvent").GetComponent<Skill>();

        VigilanceSkill.SetOwner(this);
    }

    protected override void Start()
    {
        base.Start();

        StateMachine.Init(EnemyStateType.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void OnPassiveAttackEvent()
    {
        if (++CurrentLevel > _vigilanceMaxLevel) return;

        StateMachine.ChangeState(EnemyStateType.ChestHit);
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
