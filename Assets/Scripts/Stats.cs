using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float TimeToCompleteLevel { get; set; } = 0;

    public int EnemyKilled { get; set; } = 0;

    public int MaxCombo { get; set; } = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeToCompleteLevel += Time.deltaTime;
    }

    public void PrintStats()
    {
        Debug.Log("Time:" + TimeToCompleteLevel);
        Debug.Log("EnemyKilled: " + EnemyKilled);
        Debug.Log("MaxCombo: " + MaxCombo);
    }

}
