using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float TimeToCompleteLevel { get; set; } = 0;

    public int EnemyKilled { get; set; } = 0;

    public int MaxCombo { get; set; } = 0;

    public static Stats instance;

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
    }

    public void ResetStats()
    {
        TimeToCompleteLevel = 0;
        EnemyKilled = 0;
        MaxCombo = 0;
    }

}
