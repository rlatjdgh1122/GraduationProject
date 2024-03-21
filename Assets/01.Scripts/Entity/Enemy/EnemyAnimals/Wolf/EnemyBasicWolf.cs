using System;
using UnityEngine;

public enum EnemyWolfStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Dead,
    Reached,
    MustChase,
    Provoked
}

public class EnemyBasicWolf : Enemy
{
    public EnemyStateMachine<EnemyWolfStateEnum> StateMachine { get; private set; }  

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine<EnemyWolfStateEnum>();

        foreach (EnemyWolfStateEnum state in Enum.GetValues(typeof(EnemyWolfStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"EnemyWolf{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<EnemyWolfStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(EnemyWolfStateEnum.Idle);
    }



    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();



        if (IsDead)
            StateMachine.ChangeState(EnemyWolfStateEnum.Dead);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
