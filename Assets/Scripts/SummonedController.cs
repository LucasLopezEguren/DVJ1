using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonedController : MonoBehaviour
{
    public SpawnController summoner;
    public int summonValor;
    public DamageController damageController;


    void Start() {
        summoner.enemiesAlive += summonValor;    
    }

    private bool isDead = false;
    void Update()
    {
        if (damageController.health <= 0 && !isDead) {
            summoner.enemiesAlive -= summonValor; 
            isDead = true;
        }
    }
}
