using System;

public class EnemyArcherPenguin : Enemy
{
    public EnemyStateMachine<EnemyPenguinStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine<EnemyPenguinStateEnum>();

        foreach (EnemyPenguinStateEnum state in Enum.GetValues(typeof(EnemyPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"EnemyArcher{typeName}State");
            //���÷���
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
}