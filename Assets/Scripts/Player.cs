using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);

        flipSprite(moveDelta.x);
        checkCollisionX();
        checkCollisionY();
    }

    private void flipSprite(float x)
    {
        if (x > 0) {
            transform.localScale = Vector3.one;
        } else if (x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void checkCollision(float x, float y, float distance)
    {
        distance = distance * Time.deltaTime;
        hit = Physics2D.BoxCast(
            transform.position,
            boxCollider.size,
            0,
            new Vector2(x, y),
            Mathf.Abs(distance),
            LayerMask.GetMask("Actor", "Blocking")
        );

        if (hit.collider == null) {
            if (x == 0) {
                transform.Translate(0, distance, 0);
            } else {
                transform.Translate(distance, 0, 0);
            }
        }
    }

    private void checkCollisionY()
    {
        checkCollision(0f, moveDelta.y, moveDelta.y);
    }

    private void checkCollisionX()
    {
        checkCollision(moveDelta.x, 0f, moveDelta.x);
    }
}
