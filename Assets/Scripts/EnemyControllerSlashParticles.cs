using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerSlashParticles : MonoBehaviour
{

    public EnemyController enemyController;

   void StartSlashParticles()
    {
        enemyController.StartSlashParticles();
    }

    void StopSlashParticles()
    {
        enemyController.StopSlashParticles();
    }
}
