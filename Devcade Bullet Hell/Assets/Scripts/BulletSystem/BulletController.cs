using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletData data;
    public PlayerWeaponHandler owner;
    private DestroyAfterTime destroy;
    private Rigidbody2D rb;

    public virtual void InitializeBullet(BulletData data, PlayerWeaponHandler playerController, Vector2 initialForce)
    {
        this.data = data;
        owner = playerController;

        //Destroy the bullet after a predetermined time
        destroy = GetComponent<DestroyAfterTime>();

        destroy.destroyTimer = data.bulletLifeTime;

        //Add initial force
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(initialForce, ForceMode2D.Impulse);
    }
}
