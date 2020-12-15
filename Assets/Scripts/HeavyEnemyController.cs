using UnityEngine;
using UnityEngine.UI;

public class HeavyEnemyController : MonoBehaviour
{
    public float movementSpeed = 3.0f;

    public float distanceToStand = 1.3f;

    public float chasingSpeed = 5.0f;

    public float rangeForChasing = 5f;

    public int damageToPlayer = 15;

    [SerializeField]
    private float timeToDissappearAfterDie = 5f;

    private bool isWalking = false;

    private bool isChasing = false;

    private bool isAttacking = false;

    private float attack = 0;

    private bool isFacingRight = false;

    public Animator anim;

    public CheckEdge checkEdge;

    private Rigidbody rigidBody;

    private DamageController damageController;

    public GameObject bloodSplash;

    private Transform targetPlayer;

    private Stats stats;

    private bool killStatsAdded = false;

    private bool canHit = false;

    private bool canFlip = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        damageController = this.GetComponent<DamageController>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim.SetBool("isDying", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateAnimations();
        CheckMovement();
        CheckDirection(transform.position.x, targetPlayer.position.x);
        CheckStartChasing();        
        if (!IsNearEdge() && IsAlive())
        {
            if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) >= distanceToStand && !isPlayingAttackAnimation())
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, movementSpeed * Time.deltaTime);
            }
            else
            {
                if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) < distanceToStand)
                {
                    isChasing = false;
                    isAttacking = true;
                }
            }
        }     
    }

    private bool isPlayingAttackAnimation()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_2");
    }

    private bool IsNearEdge()
    {
        return checkEdge.isNearEdge;
    }

    private void UpdateAnimations()
    {
        //anim.SetBool("isChasing", isChasing && !IsNearEdge());       
        anim.SetBool("isWalking", (isWalking || isChasing) && !IsNearEdge());
        anim.SetBool("isAttacking", isAttacking);
        if (isAttacking)
        {
            attack = Random.Range(0, 10);            
            anim.SetFloat("attack", attack);
        }
        if (CalculateHealth() <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("isDying", true);
        gameObject.layer = LayerMask.NameToLayer("DeadEnemies");
        if (stats && !killStatsAdded)
        {
            stats.EnemyKilled++;
            killStatsAdded = true;
        }
        Destroy(gameObject, timeToDissappearAfterDie);
    }

    private float CalculateHealth()
    {
        return damageController.health;
    }

    private bool IsAlive()
    {
        return CalculateHealth() > 0;
    }

    private void CheckMovement()
    {
        if (rigidBody.velocity.x != 0 && isChasing)
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
                if (!isFacingRight && canFlip)
                {
                    Flip();
                }
            }
            else
            {
                if (isFacingRight && canFlip)
                {
                    Flip();
                }
            }
        }
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void CheckStartChasing()
    {
        if (Vector3.Distance(transform.position, targetPlayer.position) <= rangeForChasing)
        {
            isAttacking = false;
            isChasing = true;
        }
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

    public void CanFlip()
    {
        canFlip = true;
    }

    public void CantFlip()
    {
        canFlip = false;
    }
}
