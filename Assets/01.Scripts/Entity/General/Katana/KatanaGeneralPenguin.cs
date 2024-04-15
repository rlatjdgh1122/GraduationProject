using System;
using Unity.VisualScripting;
using UnityEngine;

public class KatanaGeneralPenguin : General
{
    public EntityStateMachine<KatanaGeneralStateEnum, General> StateMachine { get; private set; }
    public TestStateMachine TestStateMachine { get; private set; }
    public DashSkill DashSkill;

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

    public override void OnPassiveAttackEvent()
    {
        TestStateMachine.ChangeState(PenguinStateType.Dash);
    }

    public override void AnimationTrigger() => TestStateMachine.CurrentState.AnimationTrigger();
}
