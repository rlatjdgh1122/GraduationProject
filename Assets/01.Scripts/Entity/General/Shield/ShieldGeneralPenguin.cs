using System;
using UnityEngine;

public class ShieldGeneralPenguin : General
{
    //
    //public EntityStateMachine<ShieldGeneralPenguinStateEnum, General> StateMachine { get; private set; }
    public TestStateMachine TestStateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        TestStateMachine = new TestStateMachine();
        Transform stateTrm = transform.Find("States");

        foreach (PenguinStateType state in Enum.GetValues(typeof(PenguinStateType)))
        {
            IState stateScript = stateTrm.GetComponent($"Penguin{state}State") as IState;
            if (stateScript == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            stateScript.SetUp(this, TestStateMachine, state.ToString());
            TestStateMachine.AddState(state, stateScript);
        }
    }

    protected override void Start()
    {
        StateInit();
    }

    protected override void Update()
    {
        base.Update();

        TestStateMachine.CurrentState.UpdateState();
    }
    public override void StateInit()
    {
        TestStateMachine.Init(PenguinStateType.Idle);
    }

    public override void AnimationTrigger() => TestStateMachine.CurrentState.AnimationTrigger();
}
