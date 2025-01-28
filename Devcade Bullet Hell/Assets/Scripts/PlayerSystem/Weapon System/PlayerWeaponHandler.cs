using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using Rewired;
public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] List<PlayerWeaponData> activeWeapons = new List<PlayerWeaponData>();
    [SerializeField] GameObject[] firePoints = new GameObject[8];

    [SerializeField] PlayerInputController input;
    [SerializeField] PlayerData data;

    private Player player;
    private bool canFire = true;
    private bool isFiring = false;
    private float fireTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        foreach (PlayerWeaponData weapon in activeWeapons)
        {
            Debug.Log("Fired: " + weapon.name);

            for (int i = 0; i < weapon.firePointsUsed; i++)
            {
                Debug.Log("Used FirePoint: " + 1);
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
