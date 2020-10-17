using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelChunks;
    public int amountChunks;
    public Text comboNumber;
    public Text comboText;
    private Color transparencyNumber;
    private Color transparencyText;
    // Start is called before the first frame update
    void Start()
    {
        int amountChunksTypes = levelChunks.Length;
        float nextPosition = 0f;
        for (int i = 0; i <= amountChunks; i++) {
            GameObject toInitiatie = levelChunks[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(amountChunksTypes)))];
            if (nextPosition == 0f) {
                nextPosition = ((toInitiatie.GetComponent<Transform>().localScale.x) / 2) - 4;
            }
            Instantiate(toInitiatie, new Vector3(nextPosition, 0, 0), Quaternion.identity);
            nextPosition = nextPosition + toInitiatie.GetComponent<Transform>().localScale.x;
        }
        comboNumber.text = "";
        comboText.text = "";
        transparencyNumber = comboNumber.color;
        transparencyNumber.a = 0.0f;
        transparencyText = comboText.color;
        transparencyText.a = 0.0f;

    }
    private float comboCountLifeTime = 3;
    private float comboCurrentTime = 0;
    private int comboCount = 0;
    // Update is called once per frame
    private float transparency;
    void Update()
    {
        if (comboCount > 3 && comboCurrentTime > comboCountLifeTime) {
            comboCount = 0;
            comboCurrentTime = 0f;
        }
        if (comboCount > 3) {
            transparency = (comboCountLifeTime - comboCurrentTime)/comboCountLifeTime;
            Debug.Log("comboCurrentTime: " + comboCurrentTime);
            Debug.Log("transparency: " + transparency);  
            Debug.Log("combo: " + comboCount);  
            comboNumber.text = comboCount.ToString();
            comboText.text = "hits";
            transparencyNumber.a = transparency;
            transparencyText.a = transparency;
            comboNumber.color = transparencyNumber;
            comboText.color = transparencyText;
        }
        if (comboCount > 0) {
            comboCurrentTime = comboCurrentTime + Time.deltaTime;
        }
    }

    public void addComboHit () {
        Debug.Log("comboCount: " + comboCount);
        comboCount++;
        comboCurrentTime = 0f;
    }
}
