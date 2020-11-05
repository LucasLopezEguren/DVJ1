using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemyController : MonoBehaviour
{
    public float movementSpeed = 3.0f;

    public float maxRange = 1000.0f;

    public Vector3 initialPosition;

    public Vector3 minPosition;

    public Vector3 maxPosition;

    public bool isFacingRight = false;

    public bool isAttacking = false;

    public int maxHealth = 100;

    private Rigidbody rb;

    public GameObject healthBarUI;

    public Slider slider;

    public GameObject bomb;

    public GameObject laser;

    private DamageController damageController;

    private DamageController damageController;

    public GameObject bloodSplash;

    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(-movementSpeed, 0, 0, ForceMode.Impulse);
        //slider.maxValue = maxHealth;
        //slider.value = CalculateHealth();
        initialPosition = transform.position;
        minPosition = new Vector3(initialPosition.x - maxRange, initialPosition.y, initialPosition.z);
        maxPosition = new Vector3(initialPosition.x + maxRange, initialPosition.y, initialPosition.z);
        damageController = this.GetComponent<DamageController>();
        InvokeRepeating(nameof(ShootBullet), 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirection();
        
        if (!IsAlive())
        {
            Destroy(gameObject);
        }
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
                Shoot();
                isAttacking = false;
            }
        }
    }

    private float CalculateHealth()
    {
        return damageController.health;
    }

    private bool IsAlive()
    {
        return CalculateHealth() > 0;
    }

    private void CheckDirection()
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
