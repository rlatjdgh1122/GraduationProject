using UnityEngine;

public class ShieldDeadState : ShieldBaseState
{
    public ShieldDeadState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        DeadEnter();

        /* foreach (var e in _penguin.FindNearestEnemy(5)) //�ϴ� �ӽ÷� 5�������� �̰͵� SO�� ������
         {
             if (!e.IsDead)
                 e.IsProvoked = false;
         }*/

        Debug.Log("�ֱ�");
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        Debug.Log("�ֱ�3");
        base.Exit();
    }
}
