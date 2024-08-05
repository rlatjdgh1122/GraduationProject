using UnityEngine;

public class EnemyMoose : Enemy
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        DamageCasterCompo.transform.localPosition = Vector3.zero;
        StateMachine.Init(EnemyStateType.Idle);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void Init()
    {
        base.Init();

        StateMachine.Init(EnemyStateType.Idle);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}