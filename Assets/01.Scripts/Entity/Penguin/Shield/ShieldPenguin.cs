using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ShieldPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Block,
    Impact,
    Stun,
}

public class ShieldPenguin : Penguin
{
    public EntityStateMachine<ShieldPenguinStateEnum, Penguin> _stateMachine { get; private set; }
      
    protected override void Awake()
    {
        base.Awake();

        _stateMachine = new EntityStateMachine<ShieldPenguinStateEnum, Penguin>();

        foreach (ShieldPenguinStateEnum state in Enum.GetValues(typeof(ShieldPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Shield{typeName}State");
            //���÷���
            var newState = Activator.CreateInstance(t, this, _stateMachine, typeName) as EntityState<ShieldPenguinStateEnum, Penguin>;

            _stateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        base.Start();

        StateInit();
    }
    protected override void Update()
    {
        _stateMachine.CurrentState.UpdateState();
    }

    public override void MustMoveToTargetPostion(Vector3 pos)
    {
        if (NavAgent != null)
        {
            IsMustMoving = true;

            NavAgent.isStopped = false;
            bool destinationSet = NavAgent.SetDestination(pos);

            _stateMachine.ChangeState(ShieldPenguinStateEnum.MustMove);
        }
    }

    public override void StateInit()
    {
        _stateMachine.Init(ShieldPenguinStateEnum.Idle);
    }

    public override void OnPassiveHealthRatioEvent()
    {
        _stateMachine.ChangeState(ShieldPenguinStateEnum.Stun);
    }

    public override void AnimationTrigger() => _stateMachine.CurrentState.AnimationFinishTrigger();
}