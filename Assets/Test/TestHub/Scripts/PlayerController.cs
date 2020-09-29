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

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, 0f);

        if(Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);


        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Horizontal"))));
    }
}
