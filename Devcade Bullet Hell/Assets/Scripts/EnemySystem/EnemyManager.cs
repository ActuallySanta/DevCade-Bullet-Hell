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

    private void SpawnEnemy(int enemyInd, EnemyRoundData data)
    {
        activeEnemies++;
        GameObject spawnedEnemy = Instantiate(data.enemiesToSpawn[enemyInd], spawnPoints[(int)Random.Range(0, spawnPoints.Count)]);

        spawnedEnemy.transform.parent = null;

        EnemyController controller = spawnedEnemy.GetComponent<EnemyController>();

        for (int i = 0; i < controller.data.maxPatrolPoints - 1; i++)
        {
            int randomPatrolPointInd = (int)Random.Range(0, activePatrolPoints.Count);
            controller.patrolPoints.Add(patrolPoints[randomPatrolPointInd]);

            activePatrolPoints.RemoveAt(randomPatrolPointInd);
        }
    }

    private IEnumerator RoundStart(EnemyRoundData roundData)
    {
        activePatrolPoints.Clear();

        foreach (Transform patrolPoint in patrolPoints)
        {
            activePatrolPoints.Add(patrolPoint);
        }

        for (int i = 0; i < roundData.enemiesToSpawn.Length; i++)
        {
            SpawnEnemy(i, roundData);
            yield return new WaitForSeconds(roundData.spawnWaitTime);
        }
    }
}
