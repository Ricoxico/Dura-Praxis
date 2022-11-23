using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxSpeed;
    public float minHeight;
    public float maxHeight;


    
    private Rigidbody rb;
    private Animator anim;
    private bool facingRight = false;
    private Transform target;
    private bool isDead = false;
    private float zForce;
    private float walkTimer;
    private float currentSpeed;

    void Start()
    {
        rb.GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        facingRight = (target.position.x < transform.position.x) ? false : true; 
        if(facingRight) 
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        walkTimer += Time.deltaTime;


    }

    private void FixedUpdate()
    {
        if(!isDead) 
        {
            Vector3 targetDistance= target.position - transform.position;
            float hForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            if (walkTimer >= Random.Range(1f, 2f))
            {
                zForce = Random.Range(-1, 2);
                walkTimer = 0;

            }
            if (Mathf.Abs(targetDistance.x) < 1.5f)
            {
                hForce = 0;
            }    
            rb.velocity = new Vector3 (hForce * currentSpeed ,0 ,zForce * currentSpeed);
            anim.SetFloat("Movement",Mathf.Abs(currentSpeed));

        }
        rb.position = new Vector3(rb.position.x, rb.position.y, Mathf.Clamp(rb.position.z, minHeight, maxHeight));
    }

    private void Reset()
    {
        currentSpeed = maxSpeed;
    }
}
