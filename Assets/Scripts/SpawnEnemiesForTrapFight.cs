using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesForTrapFight : MonoBehaviour
{
    public int timesToSpawn;

    public int numberOfEnemiesToSpawnEachTime;

    public float secondsBetweenSpawns;

    public GameObject enemy;

    private float timer;

    private List<GameObject> enemySpawned;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawned = new List<GameObject>();
        SpawnEnemies();
        timer = secondsBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if(timesToSpawn > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                SpawnEnemies();
                timer = secondsBetweenSpawns;
            }
        }
        else
        {
            if(enemySpawned.Count <= 0)
            {
                Destroy(gameObject);
            }
        }
        CheckAliveEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesToSpawnEachTime; i++)
        {
            var newEnemy = Instantiate(enemy, transform);
            newEnemy.GetComponent<EnemyController>().rangeForChasing = 100;
            enemySpawned.Add(newEnemy);
        }
        timesToSpawn--;
    }

    private void CheckAliveEnemies()
    {
       if(enemySpawned.Count > 0)
        {
            if(enemySpawned[0] == null)
            {
                enemySpawned.RemoveAt(0);
            }
        }
    }

}
