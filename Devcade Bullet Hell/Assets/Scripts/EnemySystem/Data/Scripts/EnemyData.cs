using UnityEngine;

public enum AttackForm
{
    meleeAttack,
    rangedAttack,
}

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Data/Enemy/Create New Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Tooltip("The maximum health of the enemy")]
    public float maxHealth = 3f;
    [Tooltip("The movement speed of the enemy")]
    public float moveSpeed = 5f;
    [Tooltip("How many points are awarded on death")]
    public float scoreValue = 1000f;

    [Tooltip("How many points can the enemy use to patrol through")]
    public float maxPatrolPoints;
    [Tooltip("How long will each enemy spend at each patrol point")]
    public float patrolPointWaitTime = 1f;
    [Tooltip("How long will the enemy recoil after being hurt")]
    public float enemyHurtWaitTime = .25f;
    [Tooltip("What kind of attack does the enemy use")]
    public AttackForm attackType;


    [Header("Ranged Attack")]
    [Tooltip("The bullets that the enemy can fire")]
    public GameObject[] bulletPrefabs;
    [Tooltip("The point where the attacks originates from")]
    public Transform firePoint;

    [Header("Melee Attack")]
    [Tooltip("The radius of the attack hitbox")]
    public float attackHitBoxRad;
    [Tooltip("How long will the attack last for")]
    public float attackDuration = .25f;
    [Tooltip("What layers the enemy can hit")]
    public LayerMask attackableLayers;

}
