using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Collidable : MonoBehaviour
{
    protected Rigidbody2D rb;

    public virtual void Start()
    {
        // Get a reference to the player's Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    public virtual void Update()
    {
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollide is not implemented for " + collision.gameObject.name);
    }
}
