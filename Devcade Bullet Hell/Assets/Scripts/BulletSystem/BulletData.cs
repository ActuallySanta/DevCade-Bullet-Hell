using UnityEngine;


public enum BulletType
{
    Standard,
    Explosive,
    Spawner,
    Bouncy,
}

public class BulletData : ScriptableObject
{
    [Tooltip("How much damage each bullet does")]
    public float bulletDamage = 1f;
    [Tooltip("How much force is added to the bullet on instantiation")]
    public float bulletSpeed = 20f;
    [Tooltip("How long will the bullet exist for? Enter -1 for unlimited time")]
    public float bulletLifeTime = -1f;
    [Tooltip("If the bullet does damage on hit")]
    public bool doesDamageOnHit = true;
    [Tooltip("What kind of bullet is this?")]
    public BulletType bulletEffect;
}
