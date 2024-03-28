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

        SignalHub.OnBattlePhaseStartEvent += OnBattleStartHandler;
    }


    private void Setting()
    {
        DummyStateMachine = new DummyStateMachine();

        foreach (DummyPenguinStateEnum state in Enum.GetValues(typeof(DummyPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Dummy{typeName}State");
            //���÷���
            var newState = Activator.CreateInstance(t, this, DummyStateMachine, typeName) as DummyState;

            DummyStateMachine.AddState(state, newState);
        }
    }
    private void Start()
    {
        StateInit();
    }
    public void StateInit()
    {
        DummyStateMachine.Init(DummyPenguinStateEnum.FreelyIdle);
    }
    private void OnBattleStartHandler()
    {
        if (Owner)
        {
            Owner.SetPosition(transform.position);

            Owner.gameObject.SetActive(true);
            Owner.StateInit();

            this.gameObject.SetActive(false);
        }
        else
            IsGoToHouse = true;
    }
    private void Update()
    {
        DummyStateMachine.CurrentState.UpdateState();
    }


    public void SetOwner(Penguin owner)
    {
        Owner = owner;
    }
    public void SetPostion(Vector3 pos)
    {
        transform.position = pos;
    }
    public void GoToHouse()
    {
        //���⼭ Ǯ �ִ°� �³� ����
        PoolManager.Instance.Push(this);
    }
    public void AnimationFinishTrigger() => DummyStateMachine.CurrentState.AnimationFinishTrigger();

    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStartHandler;
    }
}
