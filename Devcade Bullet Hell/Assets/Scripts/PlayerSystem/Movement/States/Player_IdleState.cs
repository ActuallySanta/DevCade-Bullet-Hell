using UnityEngine;

public class Player_IdleState : PlayerState
{
    bool isMoving;

    public Player_IdleState(string animName, Animator anim, PlayerController controller, PlayerData data, PlayerStateMachine stateMachine) : base(animName, anim, controller, data,stateMachine)
    {
    }

    public override void DoChecks()
    {
        if (controller.inputVector != Vector2.zero) isMoving = true;
        else isMoving = false;
    }

    public override void LogicUpdate()
    {
        if (isMoving) stateMachine.ChangeState(controller.moveState);
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
