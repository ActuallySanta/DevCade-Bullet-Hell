using UnityEngine;

public class EnemyHurtState : EnemyState
{
    bool doneHurting = false;

    public EnemyHurtState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        if (startTime + data.enemyHurtWaitTime >= Time.time)
        {
            doneHurting = true;
        }
    }

    public override void LogicUpdate()
    {
        if (doneHurting)
        {
            stateMachine.ChangeState(controller.idleState);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        controller.canBeHurt = false;
    }

    public override void OnExit()
    {
        base.OnExit();
        controller.canBeHurt = true;
    }

    public override void PhysicsUpdate()
    {
    }
}
