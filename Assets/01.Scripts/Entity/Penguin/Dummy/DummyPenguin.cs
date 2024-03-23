using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum DummyPenguinStateEnum
{
    Idle,
    Walk,
    Running,
    GoToHouse,
    DumbToDo,
}

[RequireComponent(typeof(NavMeshAgent))]
public class DummyPenguin : Penguin
{
    [Header("필요한 변수")]
    public int MaxNumberOfDumbAnim = 3;
    public bool IsGoToHouse = false;
    public Transform HouseTrm = null;

    public int RandomValue
    {
        get
        {
            int value = Random.Range(0, MaxNumberOfDumbAnim);
            return value;
        }
    }
    public EntityStateMachine<DummyPenguinStateEnum, DummyPenguin> StateMachine { get; private set; }
    protected override void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm?.GetComponent<Animator>();

        Setting();
    }

    private void Setting()
    {
        StateMachine = new EntityStateMachine<DummyPenguinStateEnum, DummyPenguin>();

        foreach (DummyPenguinStateEnum state in Enum.GetValues(typeof(DummyPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Dummy{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<DummyPenguinStateEnum, DummyPenguin>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(DummyPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void GoToHouse()
    {
        Destroy(gameObject);
    }
    protected override void HandleDie()
    {

    }
}
