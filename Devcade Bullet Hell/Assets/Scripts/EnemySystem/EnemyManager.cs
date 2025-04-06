using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyRoundData[] roundDatas;

    [SerializeField] Transform[] patrolPoints;

    [SerializeField] List<Transform> spawnPoints = new List<Transform>();

    List<Transform> activePatrolPoints = new List<Transform>();
    int activeEnemies = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform patrolPoint in patrolPoints)
        {
            activePatrolPoints.Add(patrolPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) StartCoroutine(RoundStart(roundDatas[0]));
    }

    /// <summary>
    /// Spawn in a single enemy
    /// </summary>
    /// <param name="enemyInd">which enemy is being spawned in the order that they are being spawned</param>
    /// <param name="data">The round data to pull from</param>
    /// <param name="pointCount">How many patrol points are given to each enemy</param>
    private void SpawnEnemy(int enemyInd, EnemyRoundData data, int pointCount)
    {
        //Increase the amount of enemies that are active at the moment
        activeEnemies++;

        //Spawn the enemy
        GameObject spawnedEnemy = Instantiate(data.enemiesToSpawn[enemyInd], spawnPoints[(int)Random.Range(0, spawnPoints.Count)]);

        //Remove the enemy's parent
        spawnedEnemy.transform.parent = null;

        //Get the enemy controller
        EnemyController controller = spawnedEnemy.GetComponent<EnemyController>();

        //Add in the patrol points

        //Add a fraction of the total available points
        for (int i = 0; i < pointCount - 1; i++)
        {
            //Get a random patrol point from the patrol points that are left
            int randomPatrolPointInd = (int)Random.Range(0, activePatrolPoints.Count);

            //Add that patrol point to the controller's patrol points
            controller.patrolPoints.Add(patrolPoints[randomPatrolPointInd]);

            //Remove that from the patrol points that can be used by other enemies
            activePatrolPoints.RemoveAt(randomPatrolPointInd);
        }
    }

    private IEnumerator RoundStart(EnemyRoundData roundData)
    {
        activePatrolPoints.Clear();

        //Refresh the active patrol points
        foreach (Transform patrolPoint in patrolPoints)
        {
            activePatrolPoints.Add(patrolPoint);
        }

        int pointCounts = patrolPoints.Length / roundData.enemiesToSpawn.Length;
 
        //Spawn each new enemy at a pre-specified rate
        for (int i = 0; i < roundData.enemiesToSpawn.Length; i++)
        {
            SpawnEnemy(i, roundData, pointCounts);
            yield return new WaitForSeconds(roundData.spawnWaitTime);
        }
    }
}
