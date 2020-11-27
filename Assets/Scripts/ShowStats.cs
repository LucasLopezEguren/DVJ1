using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStats : MonoBehaviour
{
    private Stats stats;

    public GameObject time;

    public GameObject enemyKilled;

    public GameObject maxCombo;

    // Start is called before the first frame update
    void Start()
    {
        stats = (Stats)GameObject.Find("Stats").GetComponent("Stats");
        time.GetComponent<TMPro.TextMeshProUGUI>().text = stats.TimeToCompleteLevel.ToString();
        enemyKilled.GetComponent<TMPro.TextMeshProUGUI>().text = stats.EnemyKilled.ToString();
        maxCombo.GetComponent<TMPro.TextMeshProUGUI>().text = stats.MaxCombo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
