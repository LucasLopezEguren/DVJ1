using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public float jumpForce;

    public float gravityScale;

    public Rigidbody rigidbody;

    public Animator anim;

    public Transform pivot;

    public float RotateSpeed;

    public int maxHealth = 200;

    public int currentHealth;

    public GameObject weapon;

    public HealthBar healthBar;       

    private int attackPhase = 0;

    private int shootPhase = 0;

    private GameManager gameManager;

    private Collider _collider;    

    private List<int> hasBeenHitted;

    private bool invincible = false;

    public bool canFlip = true;

    public bool canMove = true;

    public bool isFacingRight = true;

    [HideInInspector]
    public Vector3 moveDirection;

    private bool canJump = true;

    float timeToMove = 0.5f;

    float timeStunned = 0f;

    float timeNoJump = 0f;

    void Start()
    {
        hasBeenHitted = new List<int>();
        _collider = GetComponent<Collider>();
        currentHealth = maxHealth;
        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
        try
        {
            if (SceneManager.GetActiveScene().name != "hub")
            {
                gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            }
        }
        catch (System.Exception e)
        {
            //Debug.Log(e.Message);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(25);
        }
        if(!canMove && currentHealth > 0)
        {
            timeStunned += Time.deltaTime;
        }
        if(!canJump && currentHealth > 0)
        {
            timeNoJump += Time.deltaTime;
        }
        if(timeStunned >= timeToMove)
        {
            canMove = true;
            canFlip = true;
            timeStunned = 0;
        }
        if(timeNoJump >= timeToMove)
        {
            canJump = true;
        }
        if(canMove)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector3 temp = new Vector3(horizontal, 0, 0);
            temp = temp.normalized * moveSpeed * Time.deltaTime;
            rigidbody.MovePosition(transform.position + temp);
        }

        if (isGrounded() && canMove)
        {
            if (canJump && Input.GetButtonDown("Jump"))
            {
                anim.SetBool("jump", true);
                moveDirection.y = jumpForce;
            }
            anim.SetBool("touchFloor", true);
        }
        else
        {
            anim.SetBool("touchFloor", false);
            anim.SetBool("jump", false);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(isGrounded()){
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack"))
                {
                    ComboAttack();
                    anim.SetInteger("attacking", attackPhase);
                }
            }else{
                anim.SetBool("airAttack", true);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.X))
        {   
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("third_shoot"))
            {
                ComboShoot();
                anim.SetInteger("shooting", shootPhase);
            }
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            Vector3 tempi = new Vector3(1000, 0, 0);
            rigidbody.AddForce(tempi);
        }
        if (!isGrounded())
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        }
        if(rigidbody.velocity.y < -15)
        {
            moveDirection.y = -14;
        }
        rigidbody.velocity = moveDirection;
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Horizontal"))));
        anim.SetFloat("Y-Speed", moveDirection.y);
        anim.SetBool("isGrounded", isGrounded());
        anim.SetInteger("health", currentHealth);
        CheckMovementDirection();
        CheckAttackAnimation();
        CheckShootAnimation();
        CheckJumpAnimation();
        CheckStunAnimation();
        CheckDieAnimation();
    }


    private void CheckAttackAnimation()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("air_attack")) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            rigidbody.velocity = Vector3.zero;//for not falling while attacking
            canMove = false;
            canFlip = false;
        }
        else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle")) 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            attackPhase = 0;
            anim.SetInteger("attacking", attackPhase);
            canMove = true;
            canFlip = true;
        }
        

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("air_attack"))
        {
            anim.SetBool("airAttack", false);
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8)
            {
                canMove = true;
                canFlip = true;
            }
        }
    }

    private void CheckShootAnimation()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("first_shoot") || anim.GetCurrentAnimatorStateInfo(0).IsName("second_shoot")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("third_shoot"))
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            shootPhase = 0;
            anim.SetInteger("shooting", shootPhase);
            canMove = true;
            canFlip = true;
        }
        else if ((anim.GetCurrentAnimatorStateInfo(0).IsName("first_shoot") || anim.GetCurrentAnimatorStateInfo(0).IsName("second_shoot")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("third_shoot"))
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            //rigidbody.velocity = Vector3.zero;
            canMove = false;
            canFlip = false;
        }
    }

    private void CheckJumpAnimation()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("jump_up"))
        {
            anim.speed = 3.4f;
            canJump = false;
        }
        else
        {
            anim.speed = 1;
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("landing"))
        {
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                rigidbody.velocity = Vector3.zero;
                canMove = false;
                canFlip = false;
                canJump = false;
            }
            else if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                canMove = true;
                canFlip = true;
                canJump = true;
            }
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("falling"))
        {
            canJump = false;
        }
    }

    private void CheckStunAnimation()
    {
        if((anim.GetCurrentAnimatorStateInfo(0).IsName("stun_soft") || anim.GetCurrentAnimatorStateInfo(0).IsName("death")) 
        && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            invincible = true;
            canMove = false;
            canFlip = false;
            canJump = false;
        }
        if((anim.GetCurrentAnimatorStateInfo(0).IsName("stun_soft") || anim.GetCurrentAnimatorStateInfo(0).IsName("death")) 
        && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            invincible = false;
            canMove = true;
            canFlip = true;
            canJump = true;
            anim.SetInteger("dmgTaken", 0);
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("reincorp"))
        {
            anim.SetInteger("dmgTaken", 0);
            invincible = false;
        }
    }

    private void CheckDieAnimation()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9)
        {
            canMove = false;
            canFlip = false;
        }
    }

    private void ComboAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack"))
        {
            attackPhase = 2;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack"))
        {
            attackPhase = 3;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle"))
        {
            attackPhase = 0;
        }
        else
        {
            attackPhase = 1;
        }
    }

    private void ComboShoot()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("first_shoot"))
        {
            shootPhase = 2;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("second_shoot"))
        {
            shootPhase = 3;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("third_shoot"))
        {
            shootPhase = 0;
        }
        else
        {
            shootPhase = 1;
        }
    }
    public void TakeDamage(int damage)
    {
        if(!invincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            anim.SetInteger("dmgTaken", damage);
            anim.SetInteger("health", currentHealth);
            if (gameManager != null)
            {
                gameManager.ComboInterrupt();
            }
            if(currentHealth < 1)
            {
                Die();
            }
        }
    }

    void Die()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9)
        {
            SceneManager.LoadScene("hub");
        }
    }

    private void CheckMovementDirection()
    {
        if(canFlip)
        {
            if (isFacingRight && Input.GetAxis("Horizontal") < 0)
            {
                Flip();
            }
            else if (!isFacingRight && Input.GetAxis("Horizontal") > 0)
            {
                Flip();
            }
        }
    }

    private bool isGrounded()
    {
        Vector3 start = transform.position;
        float maxDistance = 0.5f;
        start.y = start.y + (Vector3.down * 0.8f).y;
        bool raycastHit = Physics.Raycast(start, Vector3.down, maxDistance);
        Vector3 end = start + (Vector3.down * maxDistance);
        Color color = Color.magenta;
        if (!raycastHit){
            color = Color.yellow;
        }
        Debug.DrawLine(start, end, color);
        return raycastHit;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }      

    public List<int> HasBeenHitted()
    {
        return hasBeenHitted;
    }

    public void ResetHitted()
    {
        hasBeenHitted.Clear();
    }   

    public void AddHitted(int hitted)
    {        
        hasBeenHitted.Add(hitted);
    }

}
