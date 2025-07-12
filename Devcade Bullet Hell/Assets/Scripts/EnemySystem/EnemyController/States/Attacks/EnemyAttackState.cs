using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class EnemyAttackState : EnemyState
{
    public EnemyAttackState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
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
    }

    protected abstract void FireWeapon();
}
