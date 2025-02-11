using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    public EnemyMeleeAttackState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (startTime + data.attackDuration <= Time.time) stateMachine.ChangeState(controller.idleState);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FireWeapon();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected override void FireWeapon()
    {
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(data.firePoint.position, data.attackHitBoxRad, data.attackableLayers);

        foreach (Collider2D col in hitObj)
        {
            //TODO Do Damage Logic
        }
    }
}
