using System;

public class GeneralState<T, G> : PenguinState<T, G> where T : Enum where G : General
{
    public GeneralState(G penguin, EntityStateMachine<T, G> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }


    //장군한테만 필요한 함수들을 넣을 수 있음
}
