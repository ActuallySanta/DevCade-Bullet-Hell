using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player/Create New Player Data")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed = 5f;
    public float maxHealth = 5f;

    [Tooltip("How long before you can fire again")]
    public float fireRate = .1f;

    [Tooltip("The time between each active weapon fires")]
    public float timeBetweenWeapons = .1f;

    [Tooltip("How long the player is invincible for")]
    public float invincibliltyCooldown = .5f;
}
