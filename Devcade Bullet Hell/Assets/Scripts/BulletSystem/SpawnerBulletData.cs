using UnityEngine;

[CreateAssetMenu(fileName = "New Spawner Bullet Data", menuName = "Data/Bullet/Create New Spawner Bullet Data")]
public class SpawnerBulletData : BulletData
{
    [Tooltip("How many times does the spawner create new bullets")]
    public float spawningRounds = 1f;
    [Tooltip("How many shots are fired per round")]
    public float shotsPerRound = 4f;
    [Tooltip("How much time between spawning rounds")]
    public float timeBetweenSpawnRounds = .5f;
    [Tooltip("What bullet is spawned from the round")]
    public GameObject spawnedBulletPrefab;
    [Tooltip("The data for the bullet that is spawned from the round")]
    public BulletData spawnedBulletData;
    [Tooltip("Does the spawned bullet create a round on destruction")]
    public bool spawnOnDestroy = true;

    [ContextMenu("SetEnumProgrammatically")]
    public void SetEnumProgrammatically()
    {
        if (bulletEffect != BulletType.Spawner)
        { 
            bulletEffect = BulletType.Spawner; // Set the enum to a specific value
            Debug.Log("Enum automatically set to: " + bulletEffect);
        }
    }

}
