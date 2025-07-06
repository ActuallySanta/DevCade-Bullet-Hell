using System.Collections;
using UnityEngine;

public class SpawnerBulletController : BulletController
{
    SpawnerBulletData spawnData;

    private void Start()
    {
        spawnData = (SpawnerBulletData)data;
    }

    public override void InitializeBullet(BulletData data, PlayerWeaponHandler playerController, Vector2 initialForce)
    {
        base.InitializeBullet(data, playerController, initialForce);
        StartCoroutine("SpawnRound");
    }

    private IEnumerator SpawnRound()
    {
        float angleToAdd = 360 / spawnData.shotsPerRound;

        for (int i = 0; i < spawnData.spawningRounds; i++)
        {
            yield return new WaitForSeconds(spawnData.timeBetweenSpawnRounds);

            for (int j = 0; j < spawnData.shotsPerRound; j++)
            {
                GameObject bullet = Instantiate(spawnData.spawnedBulletPrefab, transform.position, transform.rotation);

                bullet.transform.Rotate(new Vector3(0, 0, angleToAdd * j));

                bullet.transform.parent = null;

                BulletController bController = bullet.GetComponent<BulletController>();

                if (bController != null)
                {
                    //Initialize bullet
                    bController.InitializeBullet(spawnData.spawnedBulletData, owner, bullet.transform.up * spawnData.spawnedBulletData.bulletSpeed);
                }
            }
        }
    }
}
