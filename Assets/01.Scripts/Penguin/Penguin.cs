using System;
using UnityEngine;

public class Penguin : Entity
{
    [Header("Setting Values")]
    public float moveSpeed = 12f;
    public float attackDelay = 0.5f;
    public float attackTime = 0.5f;

    [SerializeField] private InputReader _inputReader;
    public InputReader Input => _inputReader;

    public PenguinStateMachine StateMachine;

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PenguinStateMachine();

        foreach (PenguinStateEnum state in Enum.GetValues(typeof(PenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Penguin{typeName}State");
            //¸®ÇÃ·º¼Ç
            PenguinState newState = Activator.CreateInstance(t, this, StateMachine, typeName) as PenguinState;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(PenguinStateEnum.Idle, this);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    private void OnEnable()
    {

    }

    private void OnDestroy()
    {

    }

    protected override void HandleDie()
    {
        //Á×¾úÀ»¶§ ¹¹ÇØÁÙÁö
        Debug.Log("Áê±Ý");
    }
}
