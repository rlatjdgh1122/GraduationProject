using Polyperfect.Common;

public class SumoGeneralPenguin : General
{
    private bool _canCalculate = false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        StateInit();
    }

    protected override void Update()
    {
        base.Update();

        StateMachine.CurrentState.UpdateState();

        if (IsInnerMeleeRange && !_canCalculate)
        {
            RunSecondPassive();
            _canCalculate = true;
        }
    }

    public override void StateInit()
    {
        StateMachine.Init(PenguinStateType.Idle);
    }

    public override void OnPassiveSecondEvent()
    {
        StateMachine.ChangeState(PenguinStateType.Stun);
    }

    public override void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
}
