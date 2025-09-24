using UnityEngine;

/// <summary>
/// Inherits from the EnemyController script so most basic capabilities are taken care of by the script
/// </summary>
public class MeleeEnemyController : EnemyController
{
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Start()
    {
        base.Start();

        //Override certain states to specify behaviour
        attackState = new EnemyMeleeAttackState("attack", anim, this, data, stateMachine);
        moveState = new EnemyChaseState("move", anim, this, data, stateMachine);
    }

    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Draw some basic debug information
    /// </summary>
    private void OnDrawGizmos()
    {
        if (data != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, data.attackHitBoxRad);
        }
    }
}
