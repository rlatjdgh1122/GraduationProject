using System;

public class GeneralState<T, G> : PenguinState<T, G> where T : Enum where G : General
{
    public GeneralState(G penguin, EntityStateMachine<T, G> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }


    //�屺���׸� �ʿ��� �Լ����� ���� �� ����
}
