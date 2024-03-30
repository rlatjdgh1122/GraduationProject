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
    public bool IsGoToHouse { get; set; } = false;
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
            //리플렉션
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
        //켜져있는 애들만
        if (gameObject.activeSelf)
        {
            NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            
            if (Owner)
            {
                Owner.SetPosAndRotation(transform);

                Owner.gameObject.SetActive(true);
                Owner.StateInit();

                this.gameObject.SetActive(false);
            }
            else
                IsGoToHouse = true;
        }
    }
    private void Update()
    {
        DummyStateMachine.CurrentState.UpdateState();
    }


    public void SetOwner(Penguin owner)
    {
        Owner = owner;
    }
    public void SetPostion(Transform trm)
    {
        transform.position = trm.position;
        transform.rotation = trm.rotation;
    }
    public void GoToHouse()
    {
        //여기서 풀 넣는게 맞나 싶음
        PoolManager.Instance.Push(this);
    }

    public void ChangeNavqualityToNone() //Nave Quality None으로 변경
    {
        NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    public void ChangeNavqualityToHigh() //Nave Quality HighQuality로 변경
    {
        NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }

    public void AnimationFinishTrigger() => DummyStateMachine.CurrentState.AnimationFinishTrigger();

    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStartHandler;
    }
}
