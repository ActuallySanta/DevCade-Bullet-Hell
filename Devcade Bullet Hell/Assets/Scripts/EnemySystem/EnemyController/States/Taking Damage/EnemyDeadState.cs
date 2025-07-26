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
        //Update the score
        GamePlayManager.Instance.UpdateScore(data.scoreValue);

        //Update the active enemy count
        EnemyManager.instance.activeEnemies--;


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
