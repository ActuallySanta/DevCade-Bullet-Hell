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
        moveState = new EnemyChaseState("move", anim, this, data, stateMachine);
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnDrawGizmos()
    {
        if (data != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, data.attackHitBoxRad);
        }
    }
}
