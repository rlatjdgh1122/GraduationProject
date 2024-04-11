using System;

public class ShieldGeneralPenguin : General
{
    public EntityStateMachine<ShieldGeneralPenguinStateEnum, General> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<ShieldGeneralPenguinStateEnum, General>();

        foreach (ShieldGeneralPenguinStateEnum state in Enum.GetValues(typeof(ShieldGeneralPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"ShieldGeneral{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<ShieldGeneralPenguinStateEnum, General>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateInit();
    }
    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.UpdateState();
    }
    public override void StateInit()
    {
        StateMachine.Init(ShieldGeneralPenguinStateEnum.Idle);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
