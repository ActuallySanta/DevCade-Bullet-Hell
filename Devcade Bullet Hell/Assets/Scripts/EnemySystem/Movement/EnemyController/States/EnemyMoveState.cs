using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    Transform targetPatrolPoint;

    public override void DoChecks()
    {
        if (Vector2.Distance(controller.transform.position, targetPatrolPoint.position) < .5f)
        {
            stateMachine.ChangeState(controller.idleState);
        }
    }

    public override void LogicUpdate()
    {
        controller.SetPatrolDestination(targetPatrolPoint);
    }

    public override void OnEnter()
    {
        base.OnEnter();

        int randomPatrolPoint = (int)Random.Range(0, data.maxPatrolPoints);
        targetPatrolPoint = controller.patrolPoints[randomPatrolPoint];
        controller.SetPatrolDestination(targetPatrolPoint);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
