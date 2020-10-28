using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour
{
    

    public GameObject healthBarUI;

    public Slider slider;
       
    public GameObject bloodSplash;
    
    public int maxHealth;

    public int health;

    public Animator anim;

    public bool isStunned = false;

    public bool isStillStunned = false;

    private GameManager gameManager;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = CalculateHealth();
        //StopSlashParticles();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    float CalculateHealth()
    {
        return health;
    }
    
    public void TakeDamage(int damage)
    {
        if(CalculateHealth() > 0)
        {
            if (isStunned)
            {
                isStillStunned = true;
                isStunned = false;
            }
            isStunned = true;
            health -= damage;
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            try
            {
                gameManager.AddComboHit();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
            healthBarUI.SetActive(true);
            if (health <= 0)
            {
                healthBarUI.SetActive(false);
            }
        }        
    }

    //public void StartSlashParticles()
    //{
    //    GetComponentInChildren<ParticleSystem>().Play();
    //    ParticleSystem.EmissionModule em = GetComponentInChildren<ParticleSystem>().emission;
    //    em.enabled = true;
    //}

    //public void StopSlashParticles()
    //{
    //    try
    //    {
    //        GetComponentInChildren<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    //        ParticleSystem.EmissionModule em = GetComponentInChildren<ParticleSystem>().emission;
    //        em.enabled = false;
    //    }
    //    catch (System.Exception e)
    //    {

    //    }
    //}
}
