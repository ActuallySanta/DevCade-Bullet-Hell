using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Data/Enemy/Create New Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Tooltip("The maximum health of the enemy")]
    public float maxHealth = 3f;
    [Tooltip("The movement speed of the enemy")]
    public float moveSpeed = 5f;
    [Tooltip("How many points can the enemy use to patrol through")]
    public float maxPatrolPoints;

}
