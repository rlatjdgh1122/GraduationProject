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
    public EnemyStateMachine<EnemyGorillaStateEnum> StateMachine { get; private set; }

    private GorillaVigilance _gorillaVigilance;
    private int _currentLevel = 0; 
    protected override void Awake()
    {
        base.Awake();

        _gorillaVigilance = GetComponent<GorillaVigilance>();

        StateMachine = new EnemyStateMachine<EnemyGorillaStateEnum>();

        foreach (EnemyGorillaStateEnum state in Enum.GetValues(typeof(EnemyGorillaStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"EnemyGorilla{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<EnemyGorillaStateEnum>;
            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        _gorillaVigilance.SetTarget(this);

        StateMachine.Init(EnemyGorillaStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void OnPassiveAttackEvent()
    {
        if (++_currentLevel > _vigilanceMaxLevel) return;

        _gorillaVigilance.OnVigilance();
        StateMachine.ChangeState(EnemyGorillaStateEnum.ChestHit);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
