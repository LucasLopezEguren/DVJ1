using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyAnimationEvents : MonoBehaviour
{
    public HeavyEnemyController heavyEnemyController;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = (AudioManager)GameObject.Find("AudioManager").GetComponent("AudioManager");
    }

    public void StartHit()
    {
        heavyEnemyController.StartHit();
    }

    public void StopHit()
    {
        heavyEnemyController.StopHit();
    }

    public void CanFlip()
    {
        heavyEnemyController.CanFlip();
    }

    public void CantFlip()
    {
        heavyEnemyController.CantFlip();
    }

    public void PlayStep1Sound()
    {
        audioManager.Play("HeavyEnemy_Step1");
    }

    public void PlayStep2Sound()
    {
        audioManager.Play("HeavyEnemy_Step2");
    }

    public void PlayDeadSound()
    {
        audioManager.Play("HeavyEnemy_Dead");
    }

    public void PlayAttack1Sound()
    {
        audioManager.Play("HeavyEnemy_Attack1");
    }

    public void PlayAttack2Sound()
    {
        audioManager.Play("HeavyEnemy_Attack2");
    }

}
