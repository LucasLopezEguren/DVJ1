using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public float movementSpeed = 5.0f;

    public int health;

    public int maxHealth = 100;

    public bool isWalking;

    private Rigidbody rb;

    public GameManager gameManager;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-movementSpeed, 0, 0, ForceMode.Impulse);
        health = maxHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        InvokeRepeating(nameof(ShootBullet), 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
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

    public void ShootBullet()
    {
        GameObject b = Instantiate(bullet);
        b.transform.position = transform.position;

    }
}
