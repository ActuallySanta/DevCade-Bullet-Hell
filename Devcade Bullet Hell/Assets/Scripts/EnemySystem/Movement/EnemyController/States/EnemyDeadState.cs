using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        
    }

    public override void LogicUpdate()
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        controller.canBeHurt = false;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void PhysicsUpdate()
    {
        
    }
}
