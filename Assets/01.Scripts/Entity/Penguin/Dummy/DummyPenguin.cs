using System;
using System.Collections.Generic;
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

    [SerializeField]
    private List<GameObject> _penguinArmor = new List<GameObject>();

    public PenguinInfoDataSO NotCloneInfo => _defaultInfo;

    public PenguinInfoDataSO CloneInfo { get; private set; } = null;

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
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        OutlineCompo = GetComponent<Outline>();
        Setting();


        CloneInfo = Instantiate(_defaultInfo);
        CloneInfo.Setting();
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
        //PoolManager.Instance.Push(this);
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (UIManager.Instance.CheckShowAble(UIType.Info))
        {
            PenguinManager.Instance.ShowPenguinInfoUI(this);
            OutlineCompo.enabled = true;
        }
    }

    #region PenguinArmor
    public void ArmorOn()
    {
        foreach (var armor in _penguinArmor)
        {
            armor.SetActive(true); // Assuming you want to activate the GameObject
        }
    }

    public void ArmorOff()
    {
        foreach (var armor in _penguinArmor)
        {
            armor.SetActive(false); // Assuming you want to deactivate the GameObject
        }
    }
    #endregion

    #region AgentQuality
    public void ChangeNavqualityToNone() //Nave Quality None
    {
        NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    public void ChangeNavqualityToHigh() //Nave Quality High
    {
        NavAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }

    public void SetNavmeshPriority(int priority)
    {
        NavAgent.avoidancePriority = priority;
    }
    #endregion

    public void AnimationFinishTrigger() => DummyStateMachine.CurrentState.AnimationFinishTrigger();
}
