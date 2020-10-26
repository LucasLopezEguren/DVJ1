using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1AnimationEvents : MonoBehaviour
{

    public EnemyController enemyController;

    public void StartSlashParticles()
    {
        enemyController.StartSlashParticles();
    }

    public void StopSlashParticles()
    {
        enemyController.StopSlashParticles();
    }

    public void StartHit()
    {
        enemyController.StartHit();
    }

    public void StopHit()
    {
        enemyController.StopHit();
    }
}
