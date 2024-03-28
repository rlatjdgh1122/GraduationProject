using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum DummyPenguinStateEnum
{
    FreelyIdle,
    Walk,
    GoToHouse,
    Running,
    DumbToDo,
}

[RequireComponent(typeof(NavMeshAgent))]
public class DummyPenguin : PoolableMono
{
    public Penguin Owner = null;

    [SerializeField]
    private PenguinInfoDataSO _penguinUIInfo = null;
    public PenguinInfoDataSO PenguinUIInfo => _penguinUIInfo;

    private int MaxNumberOfDumbAnim = 3;
    public bool IsGoToHouse { get; protected set; } = false;
    public Transform HouseTrm { get; private set; }
    public int RandomValue
    {
        get
        {
            int value = Random.Range(0, MaxNumberOfDumbAnim);
            return value;
        }
    }
    #region Components
    public Animator AnimatorCompo { get; protected set; }
    public NavMeshAgent NavAgent { get; protected set; }
    public Outline OutlineCompo { get; private set; }

    #endregion
    public DummyStateMachine DummyStateMachine { get; private set; }
    private void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        HouseTrm = FindObjectOfType<TentInitPos>().transform;

        NavAgent = GetComponent<NavMeshAgent>();
        AnimatorCompo = visualTrm?.GetComponent<Animator>();

        Setting();
    }


    private void Setting()
    {
        DummyStateMachine = new DummyStateMachine();

        foreach (DummyPenguinStateEnum state in Enum.GetValues(typeof(DummyPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Dummy{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, DummyStateMachine, typeName) as DummyState;

            DummyStateMachine.AddState(state, newState);
        }
    }
    private void Start()
    {
        DummyStateMachine.Init(DummyPenguinStateEnum.FreelyIdle);

        SignalHub.OnBattlePhaseStartEvent += OnBattleStartHandler;
    }
    private void OnBattleStartHandler()
    {
        Debug.Log("전투시작한다고");

        if (Owner)
        {
            Owner.SetPosition(transform.position);

            Owner.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
            IsGoToHouse = true;
    }
    private void Update()
    {
        DummyStateMachine.CurrentState.UpdateState();
    }

    public void AnimationFinishTrigger() => DummyStateMachine.CurrentState.AnimationFinishTrigger();

    public void GoToHouse()
    {
        PoolManager.Instance.Push(this);
        //Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStartHandler;
    }
}
