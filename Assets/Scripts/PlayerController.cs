using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Collider _collider;
    private Vector3 moveDirection;
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
    

    void Start()
    {
        _collider = GetComponent<Collider>();
        currentHealth = maxHealth;
        if (healthBar != null) healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentHealth -= 25;
            healthBar.SetHealth(currentHealth);
        }
        if(Input.GetKey("d")){
            rigidbody.AddForce(moveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if(Input.GetKey("a")){
            rigidbody.AddForce(-moveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, 0f);
        
        if (isGrounded() && Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle") && !anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack")) {
                ComboAttack();
                anim.SetInteger("attacking", attackPhase);
            }
            //PlayerAttack();
        }
        if (!isGrounded())
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        }
        rigidbody.velocity = moveDirection;
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Horizontal"))));
        CheckMovementDirection();
        CheckAttackAnimation();
    }

    private void CheckAttackAnimation() {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack")
            || anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle"))
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            //Debug.Log("not playing");
            attackPhase = 0;
            anim.SetInteger("attacking", attackPhase);
        }
        else {
            //Debug.Log("playing");
        }
    }

    private void ComboAttack() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("FirstAttack")) {
            attackPhase = 2;
        } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack")) {
            attackPhase = 3;
        } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AttackBackToIdle")) {
            attackPhase = 0;
        } else {
            attackPhase = 1;
        }
    }

    private void CheckMovementDirection() {
        if (isFacingRight && moveDirection.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && moveDirection.x > 0)
        {
            Flip();
        }
    }


    private bool isGrounded() {
        float extraHeightText = 0.01f;
        RaycastHit[] raycastHit = Physics.RaycastAll(_collider.bounds.center, Vector2.down, _collider.bounds.extents.y);
        Color rayColor;
        Debug.DrawLine(_collider.bounds.center, Vector2.down * (_collider.bounds.extents.y));
        if (raycastHit.Length > 0 && raycastHit[0].collider != null){
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        return raycastHit.Length > 0 && raycastHit[0].collider != null;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    
}
