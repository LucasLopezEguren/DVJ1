using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemyController : MonoBehaviour
{
    public float movementSpeed = 5.0f;

    public int health;

    public int maxHealth = 100;

    public bool isWalking;

    private Rigidbody rb;

    public GameManager gameManager;

    public GameObject healthBarUI;

    public Slider slider;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-movementSpeed, 0, 0, ForceMode.Impulse);
        health = maxHealth;
        slider.value = CalculateHealth();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        InvokeRepeating(nameof(ShootBullet), 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        slider.value = CalculateHealth();
    }

    private void CheckMovement()
    {
        if (rb.velocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("gameManager" + gameManager);
        if (gameManager == null)
        {
            Debug.Log("not null");
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        else
        {
            gameManager.addComboHit();
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
        Debug.Log("Die");
        Destroy(gameObject);
    }

    public void ShootBullet()
    {
        GameObject b = Instantiate(bullet);
        b.transform.position = transform.position;

    }

    float CalculateHealth()
    {
        return health;
    }
}
