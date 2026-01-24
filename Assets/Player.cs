using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float xInput;
    private bool isFacingRight = true;
    
    private bool canMove = true;
    private bool canJump = true;

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

    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;
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
            TryToJump();
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryToAttack();
    }

    private void TryToAttack()
    {
        if(isGrounded)
            anim.SetTrigger("attack");
    }
    private void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            // we attacking we are setting in the frame canMove to False
            // disable movement while attacking by setting velocity to 0
            // by setting velocityX to 0 we make sure the animator won't trigger the movement condition
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
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
    
    private void TryToJump()
    {
        // jump only if the character is on the ground
        // this avoids infinite jumps
        if (isGrounded && canJump)
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
