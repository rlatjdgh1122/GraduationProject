using System;
using Unity.VisualScripting;
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
    //private Penguin Owner = null;

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

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += OnBattleStartHandler;
    }
    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStartHandler;
    }
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
        StateInit();
    }
    public void StateInit()
    {
        DummyStateMachine.Init(DummyPenguinStateEnum.FreelyIdle);
    }
    private void OnBattleStartHandler()
    {
        ChangeNavqualityToNone();

        //군단에 소속된 펭귄이라면
        if (Owner)
        {
            Owner.SetPosAndRotation(transform);

            Owner.gameObject.SetActive(true);
            Owner.StateInit();

            //더미상태에서 풀리면 TentTrm(중앙)을 기준으로 다음 마우스 위치와 계산함
            Owner.MousePos = GameManager.Instance.TentTrm.position;

            this.gameObject.SetActive(false);
        }
        //군단에 소속되지 않은 펭귄이라면
        else
            IsGoToHouse = true;
    }
    private void Update()
    {
        DummyStateMachine.CurrentState.UpdateState();
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
}
