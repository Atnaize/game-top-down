using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected CapsuleCollider2D capsuleCollider2D;
    public int maxHealth = 10;
    protected int currentHealth = 10;
    protected float damage = 1;
    protected float colliderSizeX = 0.32f;
    protected float colliderSizeY = 0.32f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetupHealth();
    }


    private void SetupHealth()
    {
        this.currentHealth = this.maxHealth;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collide with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with an enemy");
            // Trigger your event here
        }
    }
}
