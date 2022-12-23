using UnityEngine;
using System.Collections;

public class Enemy : Actor
{
    public enum EnemyState
    {
        IDLE,
        CHASING,
        ATTACKING,
        RETURNING
    }

    public EnemyState currentState;
    public float chaseRange = 0.4f;
    public float attackDistance = 0.2f;
    public float returnRange = 0.8f;
    private Transform playerTransform;
    private float elapsedTime = 0;
    private int interval = 0;

    public override void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player").transform;
        moveSpeed = 0.3f;
    }

    public override void Update()
    {
        switch (currentState = DetermineState())
        {
            case EnemyState.IDLE:
                Idle();
                break;
            case EnemyState.CHASING:
                Chase();
                break;
            case EnemyState.ATTACKING:
                Attack();
                break;
            case EnemyState.RETURNING:
                Return();
                break;
        }
    }

    private EnemyState DetermineState()
    {
        float distanceToEnemy = Vector3.Distance(transform.position, playerTransform.position);
        float distanceToDefaultPosition = Vector3.Distance(transform.position, defaultPosition);

        // If the enemy is too far away from both the player and its default position, go into the idle state
        if (distanceToEnemy > chaseRange && distanceToDefaultPosition > returnRange)
            return EnemyState.RETURNING;

        // If the enemy is close enough to chase the player, go into the chasing state
        if (distanceToEnemy <= chaseRange && distanceToEnemy > attackDistance)
            return EnemyState.CHASING;

        // If the enemy is close enough to attack the player, go into the attacking state
        if (distanceToEnemy <= attackDistance)
            return EnemyState.ATTACKING;

        // If the enemy is too far away from the player, but within the return range of its default position, go into the replacement state
        if (distanceToEnemy > returnRange && distanceToDefaultPosition <= returnRange)
            return EnemyState.RETURNING;

        return EnemyState.IDLE;
    }

    private void Chase()
    {
        transform.position = Move(playerTransform.position);
    }

    protected override void Idle()
    {
        // // Calculate the elapsed time since the last movement
        // elapsedTime += Time.deltaTime;

        // // If the elapsed time is greater than the interval, move the enemy
        // if (elapsedTime >= interval)
        // {
        //     // Calculate a random offset for the enemy to move towards
        //     float offsetX = Random.Range(-0.5f, 0.5f);
        //     float offsetY = Random.Range(-0.5f, 0.5f);
        //     Vector3 targetPosition = defaultPosition + new Vector3(offsetX, offsetY, 0);

        //     // Move the enemy towards the target position
        //     transform.position = Move(targetPosition);

        //     // Reset the elapsed time and interval
        //     elapsedTime = 0;
        //     interval = Random.Range(2, 6);
        // }
    }


    private void Return()
    {
        transform.position = Move(defaultPosition);

        if (transform.position == defaultPosition)
        {
            currentState = EnemyState.IDLE;
        }
    }
}
