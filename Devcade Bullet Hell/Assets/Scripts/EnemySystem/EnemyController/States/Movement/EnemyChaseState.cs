using UnityEngine;

public class EnemyChaseState : EnemyMoveState
{
    public EnemyChaseState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        //Switch to attacking if the enemy is close enough to the target
        if (Vector3.Distance(controller.transform.position, controller.targetGameObject.position) <= data.minAttackDistance)
        {
            stateMachine.ChangeState(controller.attackState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
        base.PhysicsUpdate();

        //Move towards the target at a specified rate
        controller.transform.position = Vector3.MoveTowards(controller.transform.position, controller.targetGameObject.position, data.moveSpeed);
    }
}
