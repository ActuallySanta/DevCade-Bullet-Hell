using UnityEngine;

[CreateAssetMenu(fileName = "New Round Data", menuName = "Data/Enemy/Create New Round Data")]
public class EnemyRoundData : ScriptableObject
{
    [Tooltip("The amount of enemies to spawn in the round")]
    public GameObject[] enemiesToSpawn;

    [Tooltip("How much time will happen before the round begins")]
    public float preRoundWaitTimer = 0f;

    [Tooltip("How long between each enemy spawn")]
    public float spawnWaitTime = 5f;
}
