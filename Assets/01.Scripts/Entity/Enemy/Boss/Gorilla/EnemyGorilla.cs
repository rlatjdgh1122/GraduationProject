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

    public Skill VigilanceSkill { get; private set; }

    private int _currentLevel = 0; 

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine<EnemyGorillaStateEnum>();

        foreach (EnemyGorillaStateEnum state in Enum.GetValues(typeof(EnemyGorillaStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"EnemyGorilla{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<EnemyGorillaStateEnum>;
            StateMachine.AddState(state, newState);
        }

        VigilanceSkill = transform.Find("SkillManager").GetComponent<Skill>();
        VigilanceSkill.SetOwner(this);
    }

    protected override void Start()
    {
        StateMachine.Init(EnemyGorillaStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void OnPassiveAttackEvent()
    {
        if (++_currentLevel > _vigilanceMaxLevel) return;

        StateMachine.ChangeState(EnemyGorillaStateEnum.ChestHit);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
