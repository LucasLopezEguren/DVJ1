﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Transform targetPlayer;

    public float movementSpeed = 5.0f;

    public float distance = 1.0f;

    public int health;

    public int maxHealth = 100;

    public float damage = 10.0f;

    public bool isFacingRight = false;

    public bool isWalking;

    private bool isChasing = false;

    public float rangeForChasing = 5.0f;

    private Rigidbody rb;

    public Animator anim;

    public GameObject healthBarUI;
    public Slider slider;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        slider.value = CalculateHealth();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        UpdateAnimations();
        CheckDirection(transform.position.x, targetPlayer.position.x);
        CheckStartChasing();
        if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) >= distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, movementSpeed * Time.deltaTime);
        }
        else
        {
            if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) < distance)
            {
                isChasing = false;
            }
        }

        slider.value = CalculateHealth();
    }

    private void CheckStartChasing()
    {
        if (Vector3.Distance(transform.position, targetPlayer.position) <= rangeForChasing) isChasing = true;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
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

    private void CheckDirection(float positionX, float targetPositionX)
    {
        if (positionX < targetPositionX)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("gameManager" + gameManager);
        if (gameManager == null) {
            Debug.Log("not null");
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        } else {
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

    private void UpdateAnimations()
    {
        //anim.SetBool("isWalking", isWalking);
    }

    float CalculateHealth()
    {
        return health;
    }
}
