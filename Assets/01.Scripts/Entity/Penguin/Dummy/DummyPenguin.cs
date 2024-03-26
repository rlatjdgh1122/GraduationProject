using System;
using UnityEngine;
using UnityEngine.AI;

public enum DummyPenguinStateEnum
{
    FreelyIdle,
    Walk,
    GoToHouse,
    Running,
    DumbToDo,
}

[RequireComponent(typeof(NavMeshAgent))]
public class DummyPenguin : Penguin
{
    public EntityStateMachine<DummyPenguinStateEnum, Penguin> DummyStateMachine
    { get; private set; }
    protected override void Awake()
    {
        base.Awake();

        Transform visualTrm = transform.Find("Visual");
        HouseTrm = FindObjectOfType<TentInitPos>().transform;

        NavAgent = GetComponent<NavMeshAgent>();
        AnimatorCompo = visualTrm?.GetComponent<Animator>();

        Setting();

        
    }
 

    private void Setting()
    {
        DummyStateMachine = new EntityStateMachine<DummyPenguinStateEnum, Penguin>();

        foreach (DummyPenguinStateEnum state in Enum.GetValues(typeof(DummyPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Dummy{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, DummyStateMachine, typeName) as EntityState<DummyPenguinStateEnum, Penguin>;

            DummyStateMachine.AddState(state, newState);
        }
    }
    protected override void Start()
    {
        DummyStateMachine.Init(DummyPenguinStateEnum.FreelyIdle);

        SignalHub.OnBattlePhaseStartEvent += A;
    }
    private void A()
    {
        Debug.Log("전투시작한다고");
        IsGoToHouse = true;
        //DummyStateMachine.ChangeState(DummyPenguinStateEnum.GoToHouse);
    }
    protected override void Update()
    {
        DummyStateMachine.CurrentState.UpdateState();
    }

    protected override void HandleDie()
    {

    }
}
