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
    [Tooltip("How many bursts per fire action")]
    public float burstCount = 1f; 
    [Tooltip("How many fire points are used per fire action")]
    public float bulletsPerBurst = 1f;
    [Tooltip("How much time between each shot in a burst")]
    public float timeBetweenShots = -1f;
    [Tooltip("How much time between each angle")]
    public float timeBetweenFirePoints = 0f;

    [Tooltip("How many different evenly spaced angles should the gun fire its bullets at")]
    public float firePointsUsed = 1;
    [Tooltip("How many shots per separate angle")]
    public float shotsPerFirePoint = 1;

    [Tooltip("How long to wait between the firing input and the shot actually being fired")]
    public float chargeUpTime = 0f;

    [Tooltip("The magnitude of the screenshake per shot")]
    public float firingScreenShake = 1f;

    [Header("Weapon References")]
    [Tooltip("The prefab that is spawned when the fire action is called")]
    public GameObject bulletPrefab;
    [Tooltip("The particle spawned in when the player fires")]
    public GameObject firingParticle;
    [Tooltip("The data of the bullet being fired")]
    public BulletData bulletData;


}
