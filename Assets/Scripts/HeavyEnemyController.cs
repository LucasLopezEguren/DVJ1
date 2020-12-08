using UnityEngine;
using UnityEngine.UI;

public class HeavyEnemyController : MonoBehaviour
{
    public float movementSpeed = 3.0f;

    public float distanceToStand = 1.3f;

    public float chasingSpeed = 5.0f;

    public float rangeForChasing = 5f;

    //public float maxMovementRange = 5f;

    //public float attackRange = 1f;

    private bool isWalking = false;

    private bool isChasing = false;

    private bool isAttacking = false;

    public bool isFacingRight = false;

    //public Vector3 initialPosition;

    //public Vector3 minPosition;

    //public Vector3 maxPosition;

    public Animator anim;

    public CheckEdge checkEdge;

    private Rigidbody rigidBody;

    private DamageController damageController;

    public GameObject bloodSplash;

    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        //initialPosition = transform.position;
        //minPosition = new Vector3(initialPosition.x - maxMovementRange, initialPosition.y, initialPosition.z);
        //maxPosition = new Vector3(initialPosition.x + maxMovementRange, initialPosition.y, initialPosition.z);
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
            if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) >= distanceToStand && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"))
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

    private bool IsNearEdge()
    {
        return checkEdge.isNearEdge;
    }

    private void UpdateAnimations()
    {
        //anim.SetBool("isChasing", isChasing && !IsNearEdge());       
        anim.SetBool("isWalking", (isWalking || isChasing) && !IsNearEdge());
        anim.SetBool("isAttacking", isAttacking);
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
}
