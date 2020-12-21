using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemyController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public bool summonedUnit = false;
    public SpawnController summoner;
    public int summonValor = 1;

    public float maxMovementRange = 5f;

    public float shootingRange = 5f;

    public int experienceWhenKill = 20;

    private Vector3 initialPosition;

    private Vector3 minPosition;

    private Vector3 maxPosition;

    public bool isFacingRight = false;

    public bool isAttacking = false;

    public bool isBomb = true;

    public int maxHealth = 100;

    [SerializeField]
    private float timeToDissappearAfterDie = 5f;

    private Rigidbody rb;

    public GameObject healthBarUI;

    public Slider slider;

    public GameObject bomb;

    public GameObject laser;

    public Animator anim;

    private DamageController damageController;

    public GameObject bloodSplash;

    private Transform targetPlayer;

    private Stats stats;

    private SkillTree skillTree;

    private bool experienceAdded = false;

    private bool killStatsAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isDying", false);
        stats = (Stats)GameObject.Find("Stats").GetComponent("Stats");
        skillTree = (SkillTree)GameObject.Find("SkillTree").GetComponent("SkillTree");
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        minPosition = new Vector3(initialPosition.x - maxMovementRange, initialPosition.y, initialPosition.z);
        maxPosition = new Vector3(initialPosition.x + maxMovementRange, initialPosition.y, initialPosition.z);
        damageController = this.GetComponent<DamageController>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating(nameof(EnableAttack), 0f, 3f);
        if (summonedUnit && summoner != null) {
            summoner.enemiesAlive += summonValor;    
        }
    }

    private bool isDead = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsAlive() && summonedUnit && !isDead) {
            summoner.enemiesAlive -= summonValor; 
            isDead = true;
        }
        UpdateAnimations();
        Movement();
        CheckShootingRange();
        if (!IsAlive())
        {
            Die();
        }
    }

    private void UpdateAnimations()
    {
        if(!isAttacking) 
        { 
            anim.SetBool("isBomb", false);
            anim.SetBool("isLaser", false);
        }
        /*anim.SetBool("isWalking", !isAttacking);
        anim.SetBool("isbomb", isAttacking && isBomb);
        anim.SetBool("isLaser", isAttacking && !isBomb);
        if (isAttacking && isBomb)
        {
            anim.SetTrigger("DropBomb");
        }
        if (isAttacking && !isBomb)
        {
            anim.SetTrigger("ShootLaser");
        }*/

        if (!IsAlive())
        {
            Die();
        }
    }

    private void Die()
    {
        rb.useGravity = true;
        anim.SetBool("isDying", true);
        gameObject.layer = LayerMask.NameToLayer("DeadEnemies");
        if (stats && !killStatsAdded)
        {
            stats.EnemyKilled++;
            killStatsAdded = true;
        }

        if (skillTree && !experienceAdded)
        {
            skillTree.AddExperience(experienceWhenKill);
            experienceAdded = true;
        }
        Destroy(gameObject, timeToDissappearAfterDie);
    }

    public void Shoot()
    {
        if (isBomb)
        {
            anim.SetBool("isBomb", true);
            GameObject b = Instantiate(bomb);
            b.transform.position = transform.position;
        }
        else
        {
            anim.SetBool("isLaser", true);
            GameObject b = Instantiate(laser);
            b.transform.position = transform.position;
        }
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
                Shoot();
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
