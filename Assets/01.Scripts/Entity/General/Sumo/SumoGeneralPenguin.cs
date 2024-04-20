//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SumoGeneralPenguin : General
//{
//    public PenguinStateMachine StateMachine { get; private set; }

//    protected override void Awake()
//    {
//        base.Awake();

//        StateMachine = new PenguinStateMachine();
//        Transform stateTrm = transform.Find("States");

//        foreach (PenguinStateType state in Enum.GetValues(typeof(PenguinStateType)))
//        {
//            IState stateScript = stateTrm.GetComponent($"Penguin{state}State") as IState;
//            if (stateScript == null)
//            {
//                Debug.LogError($"There is no script : {state}");
//                return;
//            }
//            stateScript.SetUp(this, StateMachine, state.ToString());
//            StateMachine.AddState(state, stateScript);
//        }
//    }

//    protected override void Start()
//    {
//        StateInit();
//    }

//    protected override void Update()
//    {
//        base.Update();

//        StateMachine.CurrentState.UpdateState();
//    }

//    public override void StateInit()
//    {
//        StateMachine.Init(PenguinStateType.Idle);
//    }

//    //public override void OnPassiveAttackEvent()
//    //{
//    //    StateMachine.ChangeState(PenguinStateType.Dash);
//    //}

//    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
//}
