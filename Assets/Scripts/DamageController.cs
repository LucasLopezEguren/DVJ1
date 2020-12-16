using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageController : MonoBehaviour
{
    public GameObject healthBarUI;
    public bool hasDrop;
    public GameObject drop;
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
        if (slider != null) {
            slider.maxValue = maxHealth;
            slider.value = CalculateHealth();
        }
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (slider != null) {
            slider.value = CalculateHealth();
        }
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
            if (gameObject.name == "Portal") {
                Vector3 splashPositionLow = transform.position;
                Vector3 splashPositionMid = transform.position;
                Vector3 splashPositionHigh = transform.position;
                splashPositionLow.y = 1f;
                splashPositionMid.y = 4f;
                splashPositionHigh.y = 8f;
                Instantiate(bloodSplash, splashPositionLow, Quaternion.identity);
                Instantiate(bloodSplash, splashPositionMid, Quaternion.identity);
                Instantiate(bloodSplash, splashPositionHigh, Quaternion.identity);
            } else {
                Instantiate(bloodSplash, transform.position, Quaternion.identity);
            }
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
                if (hasDrop && drop != null){
                    Vector3 powerUpPosition = transform.position;
                    powerUpPosition.y = transform.position.y + 1f;
                    Instantiate(drop, powerUpPosition, transform.rotation);
                }
            }
        }
    }
}
