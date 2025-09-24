using UnityEngine;

public class EnemyPatrolState : EnemyMoveState
{
    //The patrol point that the enemy will be travelling to this iteration of the patrol state
    Transform targetPatrolPoint;

    public EnemyPatrolState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        //Check if the target point exists
        if (targetPatrolPoint == null) Debug.Log("target is null");

        //Check if the enemy has reached near the patrol point
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
     
        //Get a random patrol point
        int randomPatrolPoint = Random.Range(0, controller.patrolPoints.Count);

        //Set that as the patrol point to use this iteration
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
