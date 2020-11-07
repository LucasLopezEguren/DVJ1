﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemyController : MonoBehaviour
{
    public float movementSpeed = 3.0f;

    public float maxMovementRange = 5f;

    public float shootingRange = 5f;

    public Vector3 initialPosition;

    public Vector3 minPosition;

    public Vector3 maxPosition;

    public bool isFacingRight = false;

    public bool isAttacking = false;

    public int maxHealth = 100;

    private Rigidbody rb;

    public GameObject healthBarUI;

    public Slider slider;

    public GameObject bullet;

    private DamageController damageController;

    public GameObject bloodSplash;

    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        minPosition = new Vector3(initialPosition.x - maxMovementRange, initialPosition.y, initialPosition.z);
        maxPosition = new Vector3(initialPosition.x + maxMovementRange, initialPosition.y, initialPosition.z);
        damageController = this.GetComponent<DamageController>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating(nameof(EnableAttack), 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckShootingRange();

        if (!IsAlive())
        {
            Destroy(gameObject);
        }
    }

    public void ShootBullet()
    {
        GameObject b = Instantiate(bullet);
        b.transform.position = transform.position;

    }

    private bool IsAlive()
    {
        return CalculateHealth() > 0;
    }

    private void Movement()
    {
        if (IsAlive())
        {
            if (!isFacingRight)
            {
                if (Vector3.Distance(transform.position, minPosition) > 0f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, minPosition, movementSpeed * Time.deltaTime);
                }
                else
                {
                    Flip();
                }

            }
            else
            {
                if (Vector3.Distance(transform.position, maxPosition) > 0f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, maxPosition, movementSpeed * Time.deltaTime);
                }
                else
                {
                    Flip();
                }
            }
        }
    }

    private void EnableAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
        }
    }

    private void CheckShootingRange()
    {
        if(Vector3.Distance(transform.position, targetPlayer.position) <= shootingRange)
        {
            if (isAttacking)
            {
                ShootBullet();
                isAttacking = false;
            }
        }
    }

    private float CalculateHealth()
    {
        return damageController.health;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
