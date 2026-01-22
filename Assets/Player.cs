using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float xInput;
    private bool isFacingRight = true;


    [Header("Movement Details")]
    [SerializeField] private float moveSpeed =5f;
    [SerializeField] private float jumbForce =17;
    
    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround = 1 << 6; // this is layer 6 which is Ground

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {   
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }

    private void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0f && isFacingRight == false)
            Flip();
        else if (rb.linearVelocity.x < 0f && isFacingRight == true)
            Flip();
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }
    
    private void Jump()
    {
        // jump only if the character is on the ground
        // this avoids infinite jumps
        if (isGrounded)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumbForce);
    }

    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    /**
     * tranform.position is the center of the character
     */
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}
