using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AttackForm
{
    meleeAttack,
    rangedAttack,
}
public enum MoveType
{
    Patrol,
    Chase,
    Stationary,
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

    [Header("General Information")]
    [Tooltip("What kind of attack does the enemy use")]
    public AttackForm attackType;
    [Tooltip("What kind of movement does the enemy use")]
    public MoveType enemyMoveType;

    [Header("Ranged Attack")]
    [Tooltip("The bullets that the enemy can fire")]
    public GameObject[] bulletPrefab;
    [Tooltip("The data associated with each bullet game object")]
    public BulletData[] bulletData;

    [Tooltip("How many times the bullet prefabs array will be fired")]
    public float attackRounds;

    [Tooltip("How long the enemy will wait after returning to moving state before firing again")]
    public float attackCooldown;

    [Header("Melee Attack")]
    [Tooltip("The radius of the attack hitbox")]
    public float attackHitBoxRad;
    [Tooltip("How long will the attack last for")]
    public float attackDuration = .25f;
    [Tooltip("What layers the enemy can hit")]
    public LayerMask attackableLayers;
    [Tooltip("How much damage the attack does")]
    public float attackDamage;
    [Tooltip("How close does the enemy have to be to switch to attacking")]
    public float minAttackDistance = 0.01f;
    [Tooltip("How much force to apply to the target on hit")]
    public float knockbackForce = 15f;


}
