using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
    }

    public override void LogicUpdate()
    {
        if (startTime + data.patrolPointWaitTime >= Time.time) stateMachine.ChangeState(controller.moveState);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void PhysicsUpdate()
    {
    }
}
