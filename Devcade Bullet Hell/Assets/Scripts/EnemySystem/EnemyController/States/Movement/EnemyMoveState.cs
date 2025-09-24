using UnityEngine;

public class EnemyMoveState : EnemyState
{
    //Bool used by child classes to detect if they have made it to a defined position
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
        //Switch states if the enemy has arrived at defined position
        if (arrivedAtDestination)
        {
            stateMachine.ChangeState(controller.idleState);
        }
    }

    #region Unused States (needed to generate because of abstract states
    public override void LogicUpdate()
    {

    }

    public override void PhysicsUpdate()
    {

    }
    #endregion
}
