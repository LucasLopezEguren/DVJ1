using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Combo multipliers")]
    public float supernova = 3f;

    public float astronomical = 2f;

    public float blackHolish = 1.7f;

    public float cosmical = 1.3f;

    public float deorbital = 1.2f;

    public float TimeToCompleteLevel { get; set; } = 0f;

    public int EnemyKilled { get; set; } = 0;

    public int MaxCombo { get; set; } = 0;

    public float Score { get; set; } = 0f;

    public static Stats instance;

    public int finishedChunks = 0;

    public void AddFinishedChunk() {
        finishedChunks = finishedChunks + 1;
    }


    [HideInInspector]
    public bool LevelComplete = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!LevelComplete) TimeToCompleteLevel += Time.deltaTime;
    }

    public void PrintStats()
    {
        Debug.Log("Time:" + TimeToCompleteLevel);
        Debug.Log("EnemyKilled: " + EnemyKilled);
        Debug.Log("MaxCombo: " + MaxCombo);
        Debug.Log("Score: " + Score);
    }

    public void ResetStats()
    {
        LevelComplete = false;
        TimeToCompleteLevel = 0;
        EnemyKilled = 0;
        MaxCombo = 0;
        Score = 0;
    }

}
