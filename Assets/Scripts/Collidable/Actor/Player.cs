using UnityEngine;

public class Player : Actor
{
    public enum PlayerState
    {
        IDLE,
        MOVING,
        ATTACKING
    }

    public PlayerState currentState;
    public void FixedUpdate()
    {
        switch (currentState = DetermineState())
        {
            case PlayerState.IDLE:
                Idle();
                break;
            case PlayerState.MOVING:
                Move();
                break;
            case PlayerState.ATTACKING:
                Attack();
                break;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        Vector2 movementVelocity = movementDirection * moveSpeed * Time.deltaTime;

        transform.position += (Vector3)movementVelocity ;

        FlipSprite(horizontalInput);
    }

    private PlayerState DetermineState()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            return PlayerState.MOVING;

        if (Input.GetKeyDown(KeyCode.Space))
            return PlayerState.ATTACKING;

        return PlayerState.IDLE;
    }
}
