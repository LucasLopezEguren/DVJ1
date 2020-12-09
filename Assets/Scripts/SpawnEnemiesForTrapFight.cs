using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnEnemiesForTrapFight : MonoBehaviour
{
    public enum EnemyType
    {
        Enemy_1,
        FlyingEnemy,
        HeavyEnemy
    }

    public EnemyType enemyType;

    public float secondsForFirstSpawn = 0;

    public int timesToSpawn;

    public int numberOfEnemiesToSpawnEachTime;

    public float secondsBetweenSpawns;

    private GameObject enemyToInstatiate;

    private float timer;

    private List<GameObject> enemySpawned;

    private Object enemyObject;

    private bool hasBeenFirstSpawn = false;

    public GameObject Enemy_1;

    public GameObject FlyingEnemy;

    public GameObject HeavyEnemy;

    // Start is called before the first frame update
    void Start()
    {
        switch (enemyType)
        {
            case EnemyType.Enemy_1:
                //enemyObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemy_1.prefab", typeof(GameObject));
                //enemyToInstatiate = (GameObject)enemyObject;
                enemyToInstatiate = Enemy_1;
                break;
            case EnemyType.FlyingEnemy:
                //enemyObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/FlyingEnemy.prefab", typeof(GameObject));
                //nemyToInstatiate = (GameObject)enemyObject;
                enemyToInstatiate = FlyingEnemy;
                break;
            case EnemyType.HeavyEnemy:
                //enemyObject = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemy_1.prefab", typeof(GameObject));
                //enemyToInstatiate = (GameObject)enemyObject;
                enemyToInstatiate = HeavyEnemy;
                break;
            default:
                break;
        }
        enemySpawned = new List<GameObject>();
        if (secondsForFirstSpawn == 0)
        {
            hasBeenFirstSpawn = true;
            SpawnEnemies();
            timer = secondsBetweenSpawns;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenFirstSpawn)
        {
            if (timesToSpawn > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    SpawnEnemies();
                    timer = secondsBetweenSpawns;
                }
            }
            else
            {
                if (enemySpawned.Count <= 0)
                {
                    Destroy(gameObject);
                }
            }
            CheckAliveEnemies();
        }
        else
        {
            secondsForFirstSpawn -= Time.deltaTime;
            if (secondsForFirstSpawn <= 0)
            {
                hasBeenFirstSpawn = true;
                SpawnEnemies();
                timer = secondsBetweenSpawns;
            }
        }

    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesToSpawnEachTime; i++)
        {
            var newEnemy = Instantiate(enemyToInstatiate, transform);
            switch (enemyType)
            {
                case EnemyType.Enemy_1:
                    newEnemy.GetComponent<EnemyController>().rangeForChasing = 100;
                    break;
                case EnemyType.FlyingEnemy:
                    newEnemy.GetComponent<FlyingEnemyController>().isBomb = false;
                    break;
                case EnemyType.HeavyEnemy:
                    newEnemy.GetComponent<HeavyEnemyController>().rangeForChasing = 100;
                    break;
                default:
                    break;
            }
            enemySpawned.Add(newEnemy);
        }
        timesToSpawn--;
    }

    private void CheckAliveEnemies()
    {
        if (enemySpawned.Count > 0)
        {
            if (enemySpawned[0] == null)
            {
                enemySpawned.RemoveAt(0);
            }
        }
    }

}
