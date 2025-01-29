using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using Rewired;
public class PlayerWeaponHandler : MonoBehaviour
{
    public List<PlayerWeaponData> activeWeapons = new List<PlayerWeaponData>();
    public PlayerData data;
    
    [SerializeField] GameObject[] firePoints = new GameObject[16];

    [SerializeField] PlayerInputController input;

    private Player player;
    private bool canFire;
    private bool isFiring;
    private float fireTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canFire = true;
        isFiring = false;

        player = input.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButton("Fire") && canFire)
        {
            canFire = false;
            StartCoroutine(FireAction());
        }
    }

    private IEnumerator FireAction()
    {
        isFiring = true;

        //Fire every weapon the player has equipped
        foreach (PlayerWeaponData weapon in activeWeapons)
        {
            Debug.Log("Fired: " + weapon.name);

            for (int i = 0; i < weapon.firePointsUsed; i++)
            {
                Debug.Log("Used FirePoint: " + 1);

                GameObject bullet = Instantiate(weapon.bulletPrefab, firePoints[i].transform.position, firePoints[i].transform.rotation);

                bullet.transform.parent = null;

                Rigidbody2D bRB = bullet.GetComponent<Rigidbody2D>();

                if (bRB != null)
                {
                    //Add the predetermined force to the bullet
                    bRB.AddForce(bullet.transform.up * weapon.bulletSpeed, ForceMode2D.Impulse);

                    //Destroy the bullet after a predetermined time
                    DestroyAfterTime destroy = bullet.GetComponent<DestroyAfterTime>();
                    destroy.destroyTimer = weapon.bulletLifeTime;
                }

                yield return new WaitForSeconds(weapon.timeBetweenShots);

            }

            yield return new WaitForSeconds(data.timeBetweenWeapons);
        }

        isFiring = false;
        StartCoroutine(ResetCanFire());
    }

    private IEnumerator ResetCanFire()
    {
        yield return new WaitForSeconds(data.fireRate);
        canFire = true;
    }
}
