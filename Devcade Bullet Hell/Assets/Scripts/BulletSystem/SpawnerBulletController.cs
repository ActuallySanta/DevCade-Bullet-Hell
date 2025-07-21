using System.Collections;
using UnityEngine;

public class SpawnerBulletController : BulletController
{
    SpawnerBulletData spawnData;

    private void Start()
    {
        spawnData = (SpawnerBulletData)data;
    }

    public override void InitializeBullet(BulletData data, GameObject playerController, Vector2 initialForce)
    {
        base.InitializeBullet(data, playerController, initialForce);
        StartCoroutine("SpawnRound");
    }

    private IEnumerator SpawnRound()
    {
        //Split the directions around the bullet to evenly spread out the spawned bullets
        float angleToAdd = 360 / spawnData.shotsPerRound;

        for (int i = 0; i < spawnData.spawningRounds; i++)
        {
            //Wait between each round of spawns
            yield return new WaitForSeconds(spawnData.timeBetweenSpawnRounds);

            //Spawn all the bullets
            for (int j = 0; j < spawnData.shotsPerRound; j++)
            {
                GameObject bullet = Instantiate(spawnData.spawnedBulletPrefab, transform.position, transform.rotation);

                //Rotate the bullet to face the correct direction
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
