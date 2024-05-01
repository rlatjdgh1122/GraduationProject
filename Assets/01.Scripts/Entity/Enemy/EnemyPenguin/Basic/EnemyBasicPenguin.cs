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
    public EnemyStateMachine<EnemyPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine<EnemyPenguinStateEnum>();

        foreach (EnemyPenguinStateEnum state in Enum.GetValues(typeof(EnemyPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"EnemyPenguin{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<EnemyPenguinStateEnum>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        HealthCompo.OnHit += FindTarget;
        StateMachine.Init(EnemyPenguinStateEnum.Idle);
    }

    private void FindTarget()
    {
        FindNearestPenguin();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        HealthCompo.OnHit -= FindTarget;

    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
