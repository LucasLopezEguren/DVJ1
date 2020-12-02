using UnityEngine;
using UnityEngine.UI;

public class HeavyEnemyController : MonoBehaviour
{
    public float movementSpeed = 3.0f;

    public float chasingSpeed = 5.0f;

    public float rangeForChasing = 5f;

    public float maxMovementRange = 5f;

    public float attackRange = 1f;

    private bool isChasing = false;

    private bool isAttacking = false;

    public bool isFacingRight = false;

    public Vector3 initialPosition;

    public Vector3 minPosition;

    public Vector3 maxPosition;

    public Animator anim;

    public CheckEdge checkEdge;

    private Rigidbody rb;

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
        anim.SetBool("isDying", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //UpdateAnimations();
        Movement();
        /*if (!IsNearEdge() && IsAlive())
        {
            if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) >= attackRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Ataque1"))
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, chasingSpeed * Time.deltaTime);
            }
            else
            {
                if (isChasing && Vector3.Distance(transform.position, targetPlayer.position) < attackRange)
                {
                    isChasing = false;
                    isAttacking = true;
                }
            }
        }*/
    }

    private bool IsNearEdge()
    {
        return checkEdge.isNearEdge;
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isChasing", isChasing && !IsNearEdge());
    }

    private float CalculateHealth()
    {
        return damageController.health;
    }

    private bool IsAlive()
    {
        return CalculateHealth() > 0;
    }

    private void Movement()
    {
        if (IsAlive())
        {
            //CheckStartChasing();
            if (!isChasing)
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
            else
            {
                if (!isFacingRight)
                {

                }
            }
        }
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
