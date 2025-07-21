using UnityEngine;

public class EnemyRangedAttackState : EnemyAttackState
{
    public EnemyRangedAttackState(string animName, Animator anim, EnemyController controller, EnemyData data, EnemyStateMachine stateMachine) : base(animName, anim, controller, data, stateMachine)
    {
    }

    public override void LogicUpdate()
    {
        throw new System.NotImplementedException();
    }

    protected override void FireWeapon()
    {
        for (int i = 0; i < data.attackRounds; i++)
        {
            for (int j = 0; j < data.bulletPrefab.Length; j++)
            {
                controller.SpawnBullet(data.bulletPrefab[j], Vector2.down, data.bulletData[j]);
            }
        }
    }
}
