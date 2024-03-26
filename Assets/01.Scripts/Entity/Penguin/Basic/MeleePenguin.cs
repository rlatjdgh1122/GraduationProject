using System;
using UnityEngine;

public enum BasicPenguinStateEnum
{
    Idle,
    Move,
    MustMove,
    Chase,
    Attack,
    Dead,
    FreelyMove,
}

public class MeleePenguin : Penguin
{
    public EntityStateMachine<BasicPenguinStateEnum, Penguin> StateMachine { get; private set; }
    //public EntityStateMachine<DummyPenguinStateEnum, Penguin> DummyStateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        /*     DummyStateMachine = new EntityStateMachine<DummyPenguinStateEnum, Penguin>();

             foreach (DummyPenguinStateEnum state in Enum.GetValues(typeof(DummyPenguinStateEnum)))
             {
                 string typeName = state.ToString();
                 Type t = Type.GetType($"Dummy{typeName}State");
                 //府敲泛记
                 var newState = Activator.CreateInstance(t, this, DummyStateMachine, typeName) as EntityState<DummyPenguinStateEnum, Penguin>;

                 DummyStateMachine.AddState(state, newState);
             }*/

        StateMachine = new EntityStateMachine<BasicPenguinStateEnum, Penguin>();
        foreach (BasicPenguinStateEnum state in Enum.GetValues(typeof(BasicPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Basic{typeName}State");
            //府敲泛记
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<BasicPenguinStateEnum, Penguin>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        //OnFreelyMode();
        StateMachine.Init(BasicPenguinStateEnum.Idle);
    }
    /*
        protected override void OnFreelyMode()
        {
            //StateMachine.Init();
            Debug.Log("qwer");
            AnimatorCompo.SetBool("DummyState", true);
            StateMachine.Init(BasicPenguinStateEnum.Idle);
            DummyStateMachine.Init(DummyPenguinStateEnum.FreelyIdle);

            DummyStateMachine.CurrentState.Enter();
            //StateMachine.CurrentState.Exit();

        }
        public override void UnFreelyMode()
        {
            Debug.Log("qwer242");
            AnimatorCompo.SetBool("DummyState", false);

            StateMachine.Init(BasicPenguinStateEnum.Idle);
            DummyStateMachine.Init(DummyPenguinStateEnum.FreelyIdle);

            StateMachine.CurrentState.Enter();
        }*/
    protected override void Update()
    {
        /* if (isDummyPenguinMode)
         {
             DummyStateMachine.CurrentState.UpdateState();
         }
         else*/
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
