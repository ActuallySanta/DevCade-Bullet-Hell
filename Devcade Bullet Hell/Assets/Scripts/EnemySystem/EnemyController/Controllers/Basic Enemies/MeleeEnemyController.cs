using UnityEngine;

public class MeleeEnemyController : EnemyController
{

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Start()
    {
        base.Start();
        attackState = new EnemyMeleeAttackState("attack", anim, this, data, stateMachine);
    }

    public override void Update()
    {
        base.Update();
    }
}
