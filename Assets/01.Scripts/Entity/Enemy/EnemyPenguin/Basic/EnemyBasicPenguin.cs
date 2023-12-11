using System;
using UnityEngine;

public enum EnemyPenguinStateEnum
{
    Idle,
    Move,
    Chase,
    Attack,
    Dead,
    Reached,
    MustChase
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
        StateMachine.Init(EnemyPenguinStateEnum.Idle);
    }

    

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void EnableNavmesh()
    {
        NavAgent.enabled = true;
    }

    private void DisableNavmesh()
    {
        NavAgent.enabled = false;
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnPhaseStartEvent += DisableNavmesh;
        WaveManager.Instance.OnIceArrivedEvent += EnableNavmesh;
    }

    private void OnDisable()
    {
        WaveManager.Instance.OnPhaseStartEvent -= DisableNavmesh;
        WaveManager.Instance.OnIceArrivedEvent -= EnableNavmesh;
    }
}
