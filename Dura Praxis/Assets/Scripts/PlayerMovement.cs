using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int moveSpeed = 1;

    float horizontal;

    float vertical;

    Animator animator;

    bool facingRight;

    bool isCrouching;

    Rigidbody2D ridigbodyMC;

    float axisY;

    bool isJumping;

    public float jumpForce = 200f;

    bool isAttacking;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        ridigbodyMC = GetComponent<Rigidbody2D>();
        ridigbodyMC.Sleep();
    }

    void Start()
    {
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Movement", Mathf.Abs(horizontal != 0 ? horizontal : vertical));

        if(Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", isCrouching);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", isCrouching);
        }

        if(transform.position.y <= axisY)
        {
            Onlanding();
        }

        if(Input.GetButton("Jump") && !isJumping)
        {
            axisY = transform.position.y;
            isJumping = true;
            ridigbodyMC.gravityScale = 1.5f;
            ridigbodyMC.WakeUp();
            ridigbodyMC.AddForce(new Vector2(transform.position.x + 7.5f, jumpForce));
            animator.SetBool("IsJumping", isJumping);
        }

    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            isAttacking = true;
            if(vertical != 0 || horizontal != 0)
            {
                vertical = 0;
                horizontal = 0;
                animator.SetFloat("Movement", 0);
            }
            animator.SetTrigger("IsAttacking");

        }



        if(horizontal != 0 && !isCrouching || vertical != 0 && !isCrouching)
        {
            Vector3 movement = new Vector3(horizontal * moveSpeed, vertical * moveSpeed, 0.0f);
            transform.position = transform.position + movement * Time.deltaTime;
            
        }
        Flip(horizontal);

    }

    public void AlertObservers(string message)
    {
        if(message == "AttackEnded")
        {
            isAttacking = false;
        }
    }

    private void Flip (float horizontal)
    {
        if(horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Onlanding()
    {
        isJumping = false;
        ridigbodyMC.gravityScale = 0f;
        ridigbodyMC.Sleep();
        axisY = transform.position.y;
        animator.SetBool("IsJumping", isJumping);
    }
}
