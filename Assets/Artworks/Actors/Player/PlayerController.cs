using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private const string ANIMATION_X = "X";
    private const string ANIMATION_Y = "Y";
    private const string ANIMATION_MOVING = "isMoving";
    private const string ANIMATION_ATTACKING = "isAttacking";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (moveInput * moveSpeed * Time.fixedDeltaTime));
    }

    /**
     * Get a Vector2 from the keys pressed
     */
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        SwitchAnimation();
    }

    private void OnFire()
    {
        animator.SetTrigger(ANIMATION_ATTACKING);
    }

    private void SwitchAnimation()
    {
        animator.SetBool(ANIMATION_MOVING, moveInput != Vector2.zero);

        if (moveInput == Vector2.zero) {
            return;
        }

        spriteRenderer.flipX = moveInput.x < 0;

        animator.SetFloat(ANIMATION_X, moveInput.x);
        animator.SetFloat(ANIMATION_Y, moveInput.y);
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log("OnCollisionEnter with " + collision.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D with " + collision.gameObject.name);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D with " + collision.gameObject.name);

        if(collision.gameObject.CompareTag("Enemy"))
        {
            //Knock back
        }
    }
}
