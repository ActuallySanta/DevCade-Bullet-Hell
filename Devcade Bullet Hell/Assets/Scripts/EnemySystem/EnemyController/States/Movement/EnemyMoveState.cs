using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected bool arrivedAtDestination;

    public EnemyMoveState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }



    public override void OnEnter()
    {
        base.OnEnter();
        arrivedAtDestination = false;

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void DoChecks()
    {
        if (arrivedAtDestination)
        {
            stateMachine.ChangeState(controller.idleState);
        }
    }

    public override void LogicUpdate()
    {

    }

    public override void PhysicsUpdate()
    {

    }
}
