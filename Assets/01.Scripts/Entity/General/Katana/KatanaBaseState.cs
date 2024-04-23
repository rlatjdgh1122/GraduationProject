public class KatanaBaseState : State
{
    public KatanaBaseState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    protected General _general => _penguin as General;
}
