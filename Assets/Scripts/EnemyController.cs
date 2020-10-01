﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform targetPlayer;

    public float movementSpeed = 5.0f;

    public float distance = 1.0f;

    public float life = 100.0f;

    public float damage = 10.0f;

    public bool isFacingRight = false;

    public bool isWalking;

    private bool isChasing = false;

    public float rangeForChasing = 5.0f;

    private Rigidbody rb;

    //private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
            transform.localScale = new Vector3(1, 1, 1);
            if (!isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (isFacingRight)
            {
                Flip();
            }
            isFacingRight = false;
        }
    }

    private void UpdateAnimations()
    {
        //anim.SetBool("isWalking", isWalking);
    }
}
