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

    public bool isFacingRight = true;

    private int attackPhase = 0;

    private int shootPhase = 0;

    private GameManager gameManager;

    private Collider _collider;

    private Vector3 moveDirection;

    private List<int> hasBeenHitted;

    private bool invincible = false;

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
        if (Input.GetKey("d"))
        {
            rigidbody.AddForce(moveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey("a"))
        {
            rigidbody.AddForce(-moveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, 0f);

        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
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
        if (!isGrounded())
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
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
    }

    private void CheckAttackAnimation()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("air_attack")) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            //Debug.Log("not playing");
            attackPhase = 0;
            anim.SetInteger("attacking", attackPhase);
            anim.SetBool("airAttack", false);
        }
        else
        {
            //Debug.Log("playing");
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
        }
    }

    private void CheckJumpAnimation()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
        {
            anim.speed = 3.4f;
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("air_attack"))
        {
            anim.speed = 2.5f;
        }
        else
        {
            anim.speed = 1;
        }
    }

    private void CheckStunAnimation()
    {
        if((anim.GetCurrentAnimatorStateInfo(0).IsName("stun_soft") || anim.GetCurrentAnimatorStateInfo(0).IsName("death")) 
        && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            invincible = true;
        }
        if((anim.GetCurrentAnimatorStateInfo(0).IsName("stun_soft") || anim.GetCurrentAnimatorStateInfo(0).IsName("death")) 
        && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            invincible = false;
            anim.SetInteger("dmgTaken", 0);
        }
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("reincorp"))
        {
            anim.SetInteger("dmgTaken", 0);
            invincible = false;
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
            if(currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            SceneManager.LoadScene("hub");
        }
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && moveDirection.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveDirection.x > 0)
        {
            Flip();
        }
    }

    private bool isGrounded()
    {
        RaycastHit[] raycastHit = Physics.RaycastAll(_collider.bounds.center, Vector2.down, _collider.bounds.extents.y);
        Color rayColor;
        if (raycastHit.Length > 0 && raycastHit[0].collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawLine(_collider.bounds.center, Vector2.down * (_collider.bounds.extents.y), rayColor);
        return raycastHit.Length > 0 && raycastHit[0].collider != null;
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
