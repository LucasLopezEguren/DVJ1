﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelChunks;
    public GameObject[] FinishingChunks;
    public int amountChunks;
    public Text comboNumber;
    public Text comboText;
    public Image hitCombo;
    private Color transparencyNumber;
    private Color transparencyText;
    private bool femaleNarratorActivate;
    private Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = (Stats)GameObject.Find("Stats").GetComponent("Stats");
        try {
            femaleNarratorActivate = FindObjectOfType<CheatCodes>().femaleNarratorActivate;
        } catch {
            Debug.Log("No CheatCodes found");
        }
        int amountChunksTypes = levelChunks.Length;
        float nextPosition = 0f;
        for (int i = 0; i <= amountChunks; i++) {
            GameObject toInitiatie = levelChunks[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(amountChunksTypes)))];
            if (i == 0) {
                nextPosition = -((toInitiatie.GetComponent<BoxCollider>().size.x) / 2);
            } else {
                nextPosition = nextPosition + ((toInitiatie.GetComponent<BoxCollider>().size.x) / 2);
            }
            Instantiate(toInitiatie, new Vector3(nextPosition, 0, 0), Quaternion.identity);
            nextPosition = nextPosition + (toInitiatie.GetComponent<BoxCollider>().size.x)/2;
        }
        GameObject finishingChunk = FinishingChunks[Mathf.FloorToInt(UnityEngine.Random.Range(0f, Mathf.Round(FinishingChunks.Length)))];
        Instantiate(finishingChunk, new Vector3(nextPosition + ((finishingChunk.GetComponent<BoxCollider>().size.x) / 2) , 0, 0), Quaternion.identity);
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
    private bool superNova = false;
    void Update() {
        if (comboCount >= 3 && comboCurrentTime > comboCountLifeTime) {
            if (stats && stats.MaxCombo < comboCount)
            {
                stats.MaxCombo = comboCount;
            }
            comboCount = 0;
            comboCurrentTime = 0f;
            superNova = false;
        }
        if (comboCount >= 3) {
            transparency = (comboCountLifeTime - comboCurrentTime)/comboCountLifeTime;
            comboNumber.text = comboCount.ToString();
            if (comboCount < 5) {
                comboText.text = "";
            } else if ( comboCount < 8 ) {
                comboText.text = "Deorbital";
            } else if ( comboCount < 13 ) {
                comboText.text = "Cosmical";
            } else if ( comboCount < 21) {
                comboText.text = "Black holish";
            } else if (comboCount < 34) {
                comboText.text = "Astronomical";
            } else if (comboCount >= 34) {
                comboText.text = "SuperNova";
                if (!superNova) {
                    superNova = true;
                    if (femaleNarratorActivate) {
                        FindObjectOfType<AudioManager>().Play("FSuperNova");
                    } else {
                        FindObjectOfType<AudioManager>().Play("SuperNova");
                    }

                }
                
            }
            transparencyNumber.a = transparency;
            transparencyText.a = transparency;
            comboNumber.color = transparencyNumber;
            comboText.color = transparencyText;
        }
        if (comboCount > 0) {
            comboCurrentTime = comboCurrentTime + Time.deltaTime;
        }
        if (comboCount < 3) {
            transparency = 0;
            transparencyNumber.a = transparency;
            transparencyText.a = transparency;
            comboNumber.color = transparencyNumber;
            comboText.color = transparencyText;
        }
    }

    public void AddComboHit () {
        comboCount++;
        comboCurrentTime = 0f;
    }

    public void ComboInterrupt() {
        if (stats && stats.MaxCombo < comboCount) {
            stats.MaxCombo = comboCount;
        } 
        comboCount = 0;        
    }
}
