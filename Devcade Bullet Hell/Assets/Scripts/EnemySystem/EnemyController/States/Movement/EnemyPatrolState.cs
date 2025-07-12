using UnityEngine;

public class EnemyPatrolState : EnemyMoveState
{
    Transform targetPatrolPoint;

    public EnemyPatrolState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (targetPatrolPoint == null) Debug.Log("target is null");

        if (Vector2.Distance(controller.transform.position, targetPatrolPoint.position) < .5f)
        {
            arrivedAtDestination = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        controller.SetPatrolDestination(targetPatrolPoint);
    }

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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
