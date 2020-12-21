using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public int enemiesAlive = 0;
    public int maxEnemies = 5;

    public bool canSummon() {
        return enemiesAlive <= maxEnemies;
    }
}
