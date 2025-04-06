using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    Transform targetPatrolPoint;

    public override void OnEnter()
    {
        base.OnEnter();

        int randomPatrolPoint = Random.Range(0, controller.patrolPoints.Count);
        targetPatrolPoint = controller.patrolPoints[randomPatrolPoint];
        controller.SetPatrolDestination(targetPatrolPoint);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void DoChecks()
    {
        if (targetPatrolPoint == null) Debug.Log("target is null");

        if (Vector2.Distance(controller.transform.position, targetPatrolPoint.position) < .5f)
        {
            stateMachine.ChangeState(controller.idleState);
        }
    }

    public override void LogicUpdate()
    {
        controller.SetPatrolDestination(targetPatrolPoint);
    }

    public override void PhysicsUpdate()
    {

    }
}
