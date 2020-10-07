using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;
    public Transform pivot;
    public float RotateSpeed;

    public int maxHealth = 200;
    public int currentHealth;

    public GameObject weapon;
    public HealthBar healthBar;

    public bool isFacingRight = true;
    private int attackPhase = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentHealth -= 25;
            healthBar.SetHealth(currentHealth);
        }
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, 0f);
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            weapon.GetComponent<WeaponController>().Attack();
            attackPhase++;
            anim.SetInteger("attacking", attackPhase);
        }
        if (!controller.isGrounded)
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        }
        controller.Move(moveDirection * Time.deltaTime);
        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Horizontal"))));
        CheckMovementDirection();
        CheckAttackAnimation();
    }

    private void CheckAttackAnimation()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle"))
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            //Debug.Log("not playing");
            attackPhase = 0;
            anim.SetInteger("attacking", attackPhase);
        }
        else
        {
            //Debug.Log("playing");
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

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(10);
        }
    }

    void OnTriggerExit(Collider other)
    {

    }
}
