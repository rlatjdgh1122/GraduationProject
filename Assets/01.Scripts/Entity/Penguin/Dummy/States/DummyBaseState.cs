using UnityEngine;
using UnityEngine.AI;

public class DummyBaseState : DummyState
{
    private LayerMask m_layerMask;
    public DummyBaseState(DummyPenguin penguin, DummyStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
        m_layerMask = LayerMask.NameToLayer("Ground");
    }
    public override void Enter()
    {
        base.Enter();

        _triggerCalled = false;
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsGoToHouse)
        {
            _stateMachine.ChangeState(DummyPenguinStateEnum.GoToHouse);
            //return;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    protected void StopImmediately()
    {
        if (_navAgent != null)
        {
            if (_navAgent.isActiveAndEnabled)
            {
                _navAgent.isStopped = true;
            }
        }
    }
    protected void MoveToPosition(Vector3 pos)
    {
        if (_navAgent.isActiveAndEnabled)
        {
            _navAgent?.ResetPath();
            _navAgent?.SetDestination(pos);
        }
    }

    protected DummyPenguinStateEnum RandomState()
    {
        var RandomValue = Random.Range(0, 4);

        switch (RandomValue)
        {
            case 0: return DummyPenguinStateEnum.FreelyIdle;
            case 1: return DummyPenguinStateEnum.Walk;
            case 2: return DummyPenguinStateEnum.DumbToDo;
            case 3: return DummyPenguinStateEnum.Running;
            default: return DummyPenguinStateEnum.Walk;
        }

    }
    protected Vector3 GetRandomPoint()
    {
        NavMeshHit hit;
        Vector3 randomPoint = Random.insideUnitSphere * 10f;
        randomPoint.y = 1.9f;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            /* Vector3 dir = (hit.position - _penguin.transform.position).normalized;
             Vector3 checkPos = _penguin.transform.position + (dir * 1f);

             bool isGround = Physics.Raycast(checkPos, Vector3.down, 10f, m_layerMask);
             if (isGround)
             {
             }*/
            return hit.position;
        }
        return _penguin.transform.position;
    }

    protected bool IsSomethingInFront()
    {
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(_penguin.transform.position, _penguin.transform.forward, out hit, 2f);
        return hitSomething;
    }

}
