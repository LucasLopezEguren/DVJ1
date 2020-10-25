﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    private Transform targetPlayer;

    private bool isFacingRight = false;

    private bool isWalking = false;

    private bool isChasing = false;

    private bool isAttacking = false;

    private int stunAnim;

    private Rigidbody rb;

    public PlayerController playerController;

    public float movementSpeed = 5.0f;

    public float distance = 1.0f;

    public int health;

    public int maxHealth;

    //public float damage = 10.0f;

    public float rangeForChasing = 5.0f;

    public Animator anim;

    public GameObject healthBarUI;

    public Slider slider;

    public CheckEdge checkEdge;

    public GameObject bloodSplash;

    private DamageController damageController;

    // Start is called before the first frame update
    void Start()
    {
        damageController = this.GetComponent<DamageController>();
        rb = GetComponent<Rigidbody>();
        health = damageController.maxHealth;
        slider.maxValue = damageController.maxHealth;
        slider.value = CalculateHealth();
        StopSlashParticles();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();
        UpdateAnimations();
        if (damageController.isStunned) return;
        CheckMovement();
        CheckDirection(transform.position.x, targetPlayer.position.x);
        CheckStartChasing();
        if (!IsNearEdge())
        {
            if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) >= distance && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_1_attack"))
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, movementSpeed * Time.deltaTime);
            }
            else
            {
                if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) < distance)
                {
                    isChasing = false;
                    isAttacking = true;
                }
            }
        }
    }

    private void CheckStartChasing()
    {
        if (Vector3.Distance(transform.position, targetPlayer.position) <= rangeForChasing)
        {
            isAttacking = false;
            isChasing = true;
        }

    }

    private bool IsNearEdge()
    {
        return checkEdge.isNearEdge;
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

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", (isWalking || isChasing) && !IsNearEdge());
        if (damageController.isStunned)
        {
            stunAnim++;
        }
        else
        {
            stunAnim = 0;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stun_1") && damageController.isStunned)
        {
            damageController.isStunned = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stun_2") && damageController.isStillStunned)
        {
            damageController.isStillStunned = false;
        }
        anim.SetBool("isStunned", damageController.isStunned || damageController.isStillStunned);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("stunType", stunAnim % 2);
    }

    float CalculateHealth()
    {
        return damageController.health;
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
