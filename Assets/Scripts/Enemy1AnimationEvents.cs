using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1AnimationEvents : MonoBehaviour
{

    public EnemyController enemyController;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = (AudioManager)GameObject.Find("AudioManager").GetComponent("AudioManager");
    }

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

    public void PlayStep1Sound()
    {
        audioManager.Play("BasicEnemy_Step1");
    }

    public void PlayStep2Sound()
    {
        audioManager.Play("BasicEnemy_Step2");
    }

    public void PlayDeadSound()
    {
        audioManager.Play("BasicEnemy_Dead");
    }

    public void PlaySlashSound()
    {
        audioManager.Play("BasicEnemy_Slash");
    }
}
