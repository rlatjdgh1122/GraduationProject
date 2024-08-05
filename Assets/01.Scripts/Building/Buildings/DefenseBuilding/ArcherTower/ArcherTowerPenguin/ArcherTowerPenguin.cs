using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArcherTowerPenguinStateEnum
{
    Idle,
    Attack,
}


public class ArcherTowerPenguin : Penguin  
{
    public EntityStateMachine <ArcherTowerPenguinStateEnum, Penguin> _StateMachine { get; private set; }

    private bool isInstalled = false;

    public override void Init()
    {
        base.Init();
        isInstalled = false;
    }

    protected override void Awake()
    {
        base.Awake();

        _StateMachine = new EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin>();

        foreach (ArcherTowerPenguinStateEnum state in Enum.GetValues(typeof(ArcherTowerPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"ArcherTower{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, _StateMachine, typeName) as EntityState<ArcherTowerPenguinStateEnum, Penguin>;

            _StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        _StateMachine.Init(ArcherTowerPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        if (!isInstalled) { return; }
        _StateMachine.CurrentState.UpdateState();
    }

    public override void LookTarget()
    {
        if (CurrentTarget != null)
        {
            Vector3 directionToTarget = CurrentTarget.transform.position - transform.position;
            directionToTarget.y = 1.3f;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget,Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
        }
    }

    public void SetInstalled()
    {
        isInstalled = true;
    }

    public override void AnimationTrigger() => _StateMachine.CurrentState.AnimationFinishTrigger();

    protected override void HandleDie()
    {

    }
}
