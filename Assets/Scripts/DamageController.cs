using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour {
    public GameObject healthBarUI;
    public Slider slider;
    public GameManager gameManager;
    public GameObject bloodSplash;
    private Rigidbody rb;
    public int maxHealth = 15;
    public int health;

    
    void Start() {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = CalculateHealth();
        StopSlashParticles();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    float CalculateHealth()
    {
        return health;
    }

    public void TakeDamage(int damage) { 
        health -= damage;
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        else
        {
            gameManager.AddComboHit();
        }
        healthBarUI.SetActive(true);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //play a die animation
        Destroy(gameObject);
    }

    public void StartSlashParticles()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        ParticleSystem.EmissionModule em = GetComponentInChildren<ParticleSystem>().emission;
        em.enabled = true;
    }

    public void StopSlashParticles()
    {
        GetComponentInChildren<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ParticleSystem.EmissionModule em = GetComponentInChildren<ParticleSystem>().emission;
        em.enabled = false;
    }
}
