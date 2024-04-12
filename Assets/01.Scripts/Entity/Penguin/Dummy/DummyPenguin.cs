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
    [SerializeField]
    private PenguinInfoDataSO _defaultInfo = null;
    public PenguinInfoDataSO NotCloneInfo => _defaultInfo;

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
    private void Update()
    {
        DummyStateMachine.CurrentState.UpdateState();
    }
    public void GoToHouse()
    {
        //���⼭ Ǯ �ִ°� �³� ����
        PoolManager.Instance.Push(this);
    }

    private void OnMouseDown()
    {
        var infoData = PenguinManager.Instance.GetInfoDataByDummyPenguin<PenguinInfoDataSO>(this);
        var statData = PenguinManager.Instance.GetStatByInfoData<PenguinStat>(infoData);


    }

    #region ���� ����
    public void ChangeNavqualityToNone() //Nave Quality None���� ����) ���� ����
    {
        NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    public void ChangeNavqualityToHigh() //Nave Quality HighQuality�� ����) ���� ������
    {
        NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }
    #endregion

    public void AnimationFinishTrigger() => DummyStateMachine.CurrentState.AnimationFinishTrigger();
}
