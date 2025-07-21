using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    /// <summary>
    /// All the objects that <see langword="this"/> attack has hit this cycle
    /// </summary>
    List<GameObject> hitObjects = new List<GameObject>();

    public EnemyMeleeAttackState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        //Check to hit targets every frame
        FireWeapon();
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
        hitObjects.Clear();
    }

    protected override void FireWeapon()
    {
        //Get all the objects on the enemy's attackable layers that are within the range of the melee attack
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(data.firePoint.position, data.attackHitBoxRad, data.attackableLayers);

        foreach (Collider2D col in hitObj)
        {
            //Check if the target is a player and the player has not been hit yet
            if (col.gameObject.CompareTag("Player") && !hitObjects.Contains(col.gameObject))
            {
                //Add the target to the hit objects, preventing it from being hit again
                hitObjects.Add(col.gameObject);

                Debug.Log("Attacked: " + col.gameObject.name + " for " + data.attackDamage + " damage");

                //Do the actual damage
                PlayerController playerController = col.gameObject.GetComponent<PlayerController>();
                playerController.TakeDamage(data.attackDamage);

                //Apply knockback
                Rigidbody2D playerRb = col.gameObject.GetComponent<Rigidbody2D>();
                Knockback.DoKnockback(data.knockbackForce, controller.transform, col.transform, playerRb);
            }
        }
    }
}
