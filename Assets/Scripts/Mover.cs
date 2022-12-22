using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 vector3)
    {
        moveDelta = new Vector3(vector3.x * xSpeed, vector3.y * ySpeed, 0).normalized;

        flipSprite(moveDelta.x);

        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        checkCollision(moveDelta.x, 0f, moveDelta.x);
        checkCollision(0f, moveDelta.y, moveDelta.y);
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

        if (hit.collider != null)
            return;
        if (x == 0)
            transform.Translate(0, distance, 0);
        if (y == 0)
            transform.Translate(distance, 0, 0);
    }
}
