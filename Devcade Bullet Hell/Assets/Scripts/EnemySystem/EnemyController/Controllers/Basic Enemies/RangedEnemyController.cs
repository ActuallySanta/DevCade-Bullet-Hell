using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemyController : EnemyController
{
    protected float attackTimer;

    public override void Start()
    {
        base.Start();

        attackState = new EnemyRangedAttackState("attack", anim, this, data, stateMachine);

        moveState = new EnemyPatrolState("move", anim, this, data, stateMachine);
    }

    public override void Update()
    {
        base.Update();

        if (attackTimer > 0) attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            stateMachine.ChangeState(attackState);

            //Add random offset to the cooldown
            attackTimer = data.attackCooldown * Random.Range(0.25f, 1);
        }
    }
}
