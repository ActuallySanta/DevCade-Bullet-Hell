using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        if (Time.time >= startTime + 1.5f) controller.DestroyEnemy();
    }

    public override void LogicUpdate()
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        GamePlayManager.Instance.UpdateScore(data.scoreValue);
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
