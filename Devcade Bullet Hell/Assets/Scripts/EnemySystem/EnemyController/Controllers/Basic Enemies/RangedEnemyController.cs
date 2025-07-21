using UnityEngine;

public class RangedEnemyController : EnemyController
{


    public override void Start()
    {
        base.Start();

        attackState = new EnemyRangedAttackState("attack", anim, this, data, stateMachine);

        moveState = new EnemyPatrolState("move", anim, this, data, stateMachine);
    }
}
