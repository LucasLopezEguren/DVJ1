using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    public float movementSpeed = 5.0f;

    public float distance = 1.0f;

    public int damageToPlayer = 5;

    public float rangeForChasing = 5.0f;

    public Animator anim;

    public CheckEdge checkEdge;

    public GameObject bloodSplash;

    [SerializeField]
    private float timeToDissappearAfterDie;

    private bool canHit = false;

    private Transform targetPlayer;

    private bool isFacingRight = false;

    private bool isWalking = false;

    private bool isChasing = false;

    private bool isAttacking = false;

    private int stunAnim;

    private Rigidbody rb;

    private DamageController damageController;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        damageController = this.GetComponent<DamageController>();
        rb = GetComponent<Rigidbody>();
        anim.SetBool("isDying", false);
        StopSlashParticles();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimations();
        if (damageController.isStunned) return;
        CheckMovement();
        CheckDirection(transform.position.x, targetPlayer.position.x);
        CheckStartChasing();
        if (!IsNearEdge() && IsAlive())
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

    public bool IsAttacking()
    {
        return isAttacking;
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
        if (IsAlive())
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
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking", (isWalking || isChasing) && !IsNearEdge());
        if (damageController.isStunned && !isWalking)
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
            stunAnim = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stun_2") && damageController.isStillStunned)
        {
            damageController.isStillStunned = false;
            stunAnim = 0;
        }
        anim.SetBool("isStunned", (damageController.isStunned || damageController.isStillStunned) && CalculateHealth() > 0);
        if (damageController.isStunned || damageController.isStillStunned) StopSlashParticles();
        anim.SetBool("isAttacking", isAttacking);
        //if (CalculateHealth() > 0) anim.SetInteger("stunType", stunAnim % 2);
        if (CalculateHealth() > 0) stunAnim = stunAnim % 2;
        if (CalculateHealth() <= 0)
        {
            StopSlashParticles();
            anim.SetBool("isDying", true);
            gameObject.layer = LayerMask.NameToLayer("DeadEnemies");
            Destroy(gameObject, timeToDissappearAfterDie);
        }
    }

    private bool IsAlive()
    {
        return CalculateHealth() > 0;
    }

    private float CalculateHealth()
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

    public bool CanHit()
    {
        return canHit;
    }

    public void StartHit()
    {
        canHit = true;
    }

    public void StopHit()
    {
        canHit = false;
    }
}
