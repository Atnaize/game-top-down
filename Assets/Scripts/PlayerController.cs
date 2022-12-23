using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : Collidable
{
    // The different states that the player can be in
    public enum PlayerState
    {
        IDLE,
        MOVING,
        ATTACKING
    }

    public PlayerState currentState;

    // The speed at which the player moves
    public float moveSpeed = 1f;

    public override void Update()
    {
        // Check the input and update the player's state
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            currentState = PlayerState.MOVING;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = PlayerState.ATTACKING;
        }
        else
        {
            currentState = PlayerState.IDLE;
        }

        // Perform actions based on the current state
        switch (currentState)
        {
            case PlayerState.IDLE:
                // Do nothing
                break;
            case PlayerState.MOVING:
                // Move the player
                Move();
                break;
            case PlayerState.ATTACKING:
                // Jump with the player
                Attack();
                break;
        }
    }

    private void Attack()
    {

    }

    private void Move()
    {
        // Get the horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction and velocity
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        Vector2 movementVelocity = movementDirection * moveSpeed;

        // Move the player
        transform.position += (Vector3)movementVelocity * Time.deltaTime;

        flipSprite(horizontalInput);
    }

    private void flipSprite(float x)
    {
        if (x == 0) {
            return;
        }

        if (x > 0) {
            transform.localScale = Vector3.one;
            return;
        }

        transform.localScale = new Vector3(-1, 1, 1);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
    }
}
