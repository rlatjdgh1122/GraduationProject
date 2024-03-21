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

        /* foreach (var e in _penguin.FindNearestEnemy(5)) //일단 임시로 5마리도발 이것도 SO로 뺄거임
         {
             if (!e.IsDead)
                 e.IsProvoked = false;
         }*/

        Debug.Log("주금");
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        Debug.Log("주금3");
        base.Exit();
    }
}
