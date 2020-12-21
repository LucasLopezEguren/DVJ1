using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public float normalSpeed;

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

    public bool canDash = true;

    public bool isFacingRight = true;

    [HideInInspector]
    public Vector3 moveDirection;

    private bool canJump = true;

    public float jumpCooldown = 2f; 
    
    public float dashCooldown = 2f;

    public bool dashed = false;

    float endDash = 0.2f;

    float timeToMove = 0.5f;

    float timeStunned = 0f;

    float timeNoJump = 0f;

    float distToGround;

    public LayerMask Ground;

    [HideInInspector]
    public SkillTree skillTree;

    private SkillsUI skillsUI;

    public PlayerAttackEvent playerAttackEvent;

    [HideInInspector]
    public bool isDashing = false;

    private int jumps = 2;

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
        distToGround = _collider.bounds.extents.y;
        normalSpeed = moveSpeed;
        skillTree = (SkillTree)GameObject.Find("SkillTree").GetComponent("SkillTree");
        skillsUI = (SkillsUI)GameObject.Find("SkillsUI").GetComponent("SkillsUI");
        playerAttackEvent.typeOfShoot = PlayerAttackEvent.TypeOfShoot.normal;
    }

    void FixedUpdate()
    {
        if(canMove && currentHealth > 0)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector3 temp = new Vector3(horizontal, 0, 0);
            temp = temp.normalized * moveSpeed * Time.deltaTime;
            rigidbody.MovePosition(transform.position + temp);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Dash();
        }
        
        CheckDashAnimation();
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
        if(jumpCooldown < 1.5f)
        {
            jumpCooldown += Time.deltaTime;
        }
        if(dashCooldown < 1.5f)
        {
            dashCooldown += Time.deltaTime;
        }
        if(endDash < 0.2f)
        {
            endDash += Time.deltaTime;
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
        if(canMove && currentHealth > 0)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector3 temp = new Vector3(horizontal, 0, 0);
            temp = temp.normalized * moveSpeed * Time.deltaTime;
            rigidbody.MovePosition(transform.position + temp);
        }
        if (isGrounded() && canMove )
        {
            if (canJump && currentHealth > 0 && Input.GetButtonDown("Jump"))
            {
                if(jumpCooldown >= 1.5f)
                {
                    anim.SetBool("jump", true);
                    moveDirection.y = jumpForce;
                    jumpCooldown = 0f;
                }
                jumps--;
            }
            jumps=2;
        }
        else
        {
            if(skillTree.skills.doubleJump && jumps > 1 && currentHealth > 0 && Input.GetButtonDown("Jump"))
            {
                anim.SetBool("jump", true);
                moveDirection.y = jumpForce;
                jumps--;
            }
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
                playerAttackEvent.typeOfShoot = PlayerAttackEvent.TypeOfShoot.normal;
                ComboShoot();
                anim.SetInteger("shooting", shootPhase);
            }
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
        //CheckDashAnimation();
        CheckSkills();
    }

    private void Dash()
    {
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && dashCooldown >= 1.5f && canMove && attackPhase == 0 && shootPhase == 0)
        {
            anim.SetTrigger("dash");
            moveSpeed = moveSpeed * 2;
            dashCooldown = 0;
            gravityScale = 0;
        }
    }

    private void CheckDashAnimation()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("dash") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9)
        {
            anim.ResetTrigger("dash");
            moveSpeed = normalSpeed;
            invincible = false;
            canDash = true;
            canFlip = true;
            canMove = true;
            isDashing = false;
            gravityScale = 5;
        }
        
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("dash") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9)
        {
            rigidbody.velocity = Vector3.zero;
            canMove = false;
            canFlip = false;
            canDash = false;
            invincible = true;
        }
    }

    private void CheckAttackAnimation()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle")
            /*|| anim.GetCurrentAnimatorStateInfo(0).IsName("air_attack")*/) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
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
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("air_attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6)
        {
            rigidbody.velocity = Vector3.zero;
            canMove = false;
            canFlip = false;
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
            playerAttackEvent.PlayFallSound();
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
        if((anim.GetCurrentAnimatorStateInfo(0).IsName("stun_soft") || anim.GetCurrentAnimatorStateInfo(0).IsName("death")
        || anim.GetCurrentAnimatorStateInfo(0).IsName("reincorp")) 
        && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            invincible = true;
            canMove = false;
            canFlip = false;
            canJump = false;
        }
        if((anim.GetCurrentAnimatorStateInfo(0).IsName("stun_soft") || anim.GetCurrentAnimatorStateInfo(0).IsName("death")
        || anim.GetCurrentAnimatorStateInfo(0).IsName("reincorp")) 
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
        if(canFlip && currentHealth > 0)
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
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, Ground.value);
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

    void OnCollisionEnter(Collision collision)
    {
        if(moveSpeed > normalSpeed)
        {
            moveSpeed = normalSpeed;
        }
    }

    private void CheckSkills()
    {
        if (skillTree.skills.shield)
        {
            if (skillsUI.selectedSkill.name == "Shield" && Input.GetKeyDown(KeyCode.LeftControl) && !skillTree.skills.shieldActive)
            {
                skillTree.skills.shieldActive = true;
                skillTree.skills.timerShield = skillTree.skills.timeOfShield;
                invincible = true;
            }
            if (skillTree.skills.shieldActive)
            {
                skillTree.skills.timerShield -= Time.deltaTime;
                if (skillTree.skills.timerShield <= 0)
                {
                    skillTree.skills.shieldActive = false;
                    invincible = false;
                }
            }
        }
        if (skillTree.skills.rage)
        {
            if (skillsUI.selectedSkill.name == "Rage" && Input.GetKeyDown(KeyCode.LeftControl) && !skillTree.skills.rageActive)
            {
                skillTree.skills.rageActive = true;
                skillTree.skills.timerRage = skillTree.skills.timeOfRage;
            }
            if (skillTree.skills.rageActive)
            {
                skillTree.skills.timerRage -= Time.deltaTime;
                if (skillTree.skills.timerRage <= 0)
                {
                    skillTree.skills.rageActive = false;
                }
            }
        }
        if (skillTree.skills.grenade)
        {
            if (skillsUI.selectedSkill.name == "Grenade" && Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("third_shoot"))
                {
                    playerAttackEvent.typeOfShoot = PlayerAttackEvent.TypeOfShoot.grenade;
                    ComboShoot();
                    anim.SetInteger("shooting", shootPhase);
                }
            }
        }
        if (skillTree.skills.laser)
        {
            if (skillsUI.selectedSkill.name == "Laser" && Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("third_shoot"))
                {
                    playerAttackEvent.typeOfShoot = PlayerAttackEvent.TypeOfShoot.laser;
                    ComboShoot();
                    anim.SetInteger("shooting", shootPhase);
                }
            }
        }
        //if (skillTree.skills.doubleJump)
        //{
        //    if(doubleJump == 1 && Input.GetButtonDown("Jump"))
        //    {
        //        doubleJump--;
        //        anim.SetBool("jump", true);
        //        moveDirection.y = jumpForce;
        //    }
        //}
    } 
}
