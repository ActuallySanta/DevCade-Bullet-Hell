using UnityEngine;

[CreateAssetMenu(fileName = "New Player Weapon Data", menuName = "Data/Player/Create New Player Weapon Data")]
public class PlayerWeaponData : ScriptableObject
{
    [Header("Weapon Info")]
    [Tooltip("The name of the weapon")]
    public string weaponName;
    [Tooltip("A description of the weapon")]
    public string weaponInfo; 

    [Header("Weapon Stats")]
    [Tooltip("How much damage each bullet does")]
    public float bulletDamage = 1f;
    [Tooltip("How many bursts per fire action")]
    public float burstCount = 1f; 
    [Tooltip("How many fire points are used per fire action")]
    public float bulletsPerBurst = 1f;

    [Tooltip("How many fire points are used per fire action")]
    [Range(0,8)]
    public int firePointsUsed = 1; 

    [Tooltip("The magnitude of the screenshake per shot")]
    public float firingScreenShake = 1f;

    [Header("Weapon References")]
    [Tooltip("The prefab that is spawned when the fire action is called")]
    public GameObject bulletPrefab;
    [Tooltip("The particle spawned in when the player fires")]
    public GameObject firingParticle;


}
