using UnityEngine;

public class EnemyRoundData : ScriptableObject
{
    [Tooltip("The amount of enemies to spawn in the round")]
    public GameObject[] enemiesToSpawn;

    [Tooltip("How long the round lasts")]
    public float roundTimer = 10f;
    [Tooltip("How long between each enemy spawn")]
    public float spawnWaitTime = 5f;
}
