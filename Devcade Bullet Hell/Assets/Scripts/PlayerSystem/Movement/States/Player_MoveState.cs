using UnityEngine;

public class Player_MoveState : PlayerState
{

    public Player_MoveState(string animName, Animator anim, PlayerController controller, PlayerData data, PlayerStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        if (controller.inputVector == Vector2.zero) stateMachine.ChangeState(controller.idleState);
    }

    public override void LogicUpdate()
    {

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
        controller.SetVelocity(data.moveSpeed);
    }

    
}
