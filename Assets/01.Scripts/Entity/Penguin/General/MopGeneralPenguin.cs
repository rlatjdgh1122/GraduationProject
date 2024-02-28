using System;
using UnityEngine;
using UnityEngine.Rendering;

public class MopGeneralPenguin : General
{
    public EntityStateMachine<GeneralPenguinStateEnum,General> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EntityStateMachine<GeneralPenguinStateEnum,General>();

        foreach (GeneralPenguinStateEnum state in Enum.GetValues(typeof(GeneralPenguinStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"General{typeName}State");
            //리플렉션
            var newState = Activator.CreateInstance(t, this, StateMachine, typeName) as EntityState<GeneralPenguinStateEnum, General>;

            StateMachine.AddState(state, newState);
        }
    }

    protected override void Start()
    {
        StateMachine.Init(GeneralPenguinStateEnum.Idle);
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.UpdateState();


        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(ArmyManager.Instance.GetCurArmy().Legion);
            Debug.Log(ArmyManager.Instance.GetCurArmy().Soldiers.Count);

            ArmyManager.Instance.GetCurArmy().AddStat(ligeonStat);
        }
    }

    public override void OnPassiveAttackEvent()
    {
        StateMachine.ChangeState(GeneralPenguinStateEnum.AoEAttack);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
