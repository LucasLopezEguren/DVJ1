using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour
{
    public GameObject healthBarUI;

    public Slider slider;

    public int maxHealth;

    public int health;

    public bool isStunned = false;

    public bool isStillStunned = false;

    public GameObject bloodSplash;

    private GameManager gameManager;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = CalculateHealth();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        slider.value = CalculateHealth();
    }

    public float CalculateHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        if (CalculateHealth() > 0)
        {
            if (isStunned)
            {
                isStillStunned = true;
                isStunned = false;
            }
            else
            {
                isStillStunned = false;
                isStunned = true;
            }            
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
}
