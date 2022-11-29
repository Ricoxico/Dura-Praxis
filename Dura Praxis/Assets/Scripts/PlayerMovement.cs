using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D ridigbodyMC;
    Animator animator;

    public int moveSpeed = 1;
    float horizontal;
    float vertical;
    bool facingRight;
    float axisY;
    bool isCrouching;
    bool isJumping;
    public float jumpForce = 200f;
    bool isAttacking;
    float countSlider;
    public float slideCooldown;
    private float lastSlide;
    int hp;

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

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            animator.SetBool("IsSliding", false);
            animator.SetBool("IsCrouching", isCrouching);
        }
        else if (Input.GetButtonDown("Slider"))
        {
            if (Time.time - lastSlide < slideCooldown)
            {
                return;
            }
            lastSlide = Time.time;
            countSlider = 0.5f;
            animator.SetFloat("Movement", 0.0f);
            animator.SetBool("IsSliding", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", isCrouching);
        }

        if (countSlider > 0)
        {
            animator.SetFloat("Movement", 0.0f);
            countSlider = countSlider - (1f * Time.deltaTime);
            if (countSlider <= 0)
            {
                animator.SetBool("IsSliding", false);
            }
        }

        if (transform.position.y <= axisY)
        {
            Onlanding();
        }

        if (Input.GetButton("Jump") && !isJumping)
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
            if (vertical != 0 || horizontal != 0)
            {
                vertical = 0;
                horizontal = 0;
                animator.SetFloat("Movement", 0);
            }
            animator.SetTrigger("IsAttacking");

        }

        if (horizontal != 0 && !isCrouching || vertical != 0 && !isCrouching)
        {
            Vector3 movement = new Vector3(horizontal * moveSpeed, vertical * moveSpeed, 0.0f);
            transform.position = transform.position + movement * Time.deltaTime;
        }
        Flip(horizontal);
    }

    public void AlertObservers(string message)
    {
        if (message == "AttackEnded")
            isAttacking = false;
    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && !facingRight || horizontal > 0 && facingRight)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fino")
        {
            Heal();
            Destroy(GameObject.FindWithTag("Fino"));
        }
    }

    private void Heal()
    {
        hp = 100;
        HpManager.instance.ChangeHealth(100);
    }
}
