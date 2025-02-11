using UnityEngine;

public abstract class EnemyAttackState : EnemyState
{
    public EnemyAttackState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void DoChecks()
    {
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        FireWeapon();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }

    protected abstract void FireWeapon();
}
