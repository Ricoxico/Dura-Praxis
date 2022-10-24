using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int moveSpeed = 1;

    float horizontal;
    float vertical;





    void Start()
    {
        
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontal * moveSpeed, vertical * moveSpeed , 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;

    }
}
