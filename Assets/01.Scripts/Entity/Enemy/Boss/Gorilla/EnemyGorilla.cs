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

    private int _currentLevel = 0; 

    protected override void Awake()
    {
        base.Awake();

       
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
        if (++_currentLevel > _vigilanceMaxLevel) return;

        StateMachine.ChangeState(EnemyStateType.ChestHit);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
