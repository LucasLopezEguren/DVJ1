﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    
    private float cheatInputLifeTime = 1.5f;
    private float cheatInputCurrentTime = 0;
    private int cheatInputCount = 0;

    [HideInInspector]
    public bool maleNarratorActivate = false;
    public bool cheatEnabled = false;
    void Awake() {
        DontDestroyOnLoad(gameObject);   
    }

    
    void Update() {
        if (cheatInputCount > 0){
            cheatInputCurrentTime += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && cheatInputCount == 0) {
            cheatInputCount++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && cheatInputCount == 1) {
            cheatInputCount++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && cheatInputCount == 2) {
            cheatInputCount++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && cheatInputCount == 3) {
            cheatInputCount++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && cheatInputCount == 4) {
            cheatInputCount++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && cheatInputCount == 5) {
            cheatInputCount++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z) && cheatInputCount == 6) {
            cheatInputCount++;
            return;
        }
        if (Input.GetKeyDown(KeyCode.X) && cheatInputCount == 7) {
            cheatInputCount++;
            return;
        }
        if (cheatInputCount == 8 && !maleNarratorActivate) {
            maleNarratorActivate = true;
            Debug.Log("Cheat enabled!");
        }
        if ((Input.anyKeyDown || cheatInputCurrentTime >= cheatInputLifeTime) && cheatInputCount > 0 && cheatInputCount < 8) {
            Debug.Log("fail in: " + cheatInputCount);
            cheatInputCount = 0;
            cheatInputCurrentTime = 0;
            return;
        }

        if (maleNarratorActivate && !cheatEnabled) {
            AudioManager test = FindObjectOfType<AudioManager>();
            test.Play("FSuperNova");
            cheatEnabled = true;
        }
    }
}
