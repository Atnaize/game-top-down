using UnityEngine;

public class Actor : Collidable
{
    public float moveSpeed = 0.5f;
    public Vector3 defaultPosition;

    public override void Start()
    {
        base.Start();
        defaultPosition = transform.position;
        Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer("Weapon"), 0);
    }

    protected virtual void Attack()
    {
    }

    protected virtual void Idle()
    {
    }

    protected virtual Vector3 Move(Vector3 target)
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);
        FlipSprite(newPosition.x - transform.position.x);

        return newPosition;
    }

    protected void FlipSprite(float x)
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
        Debug.Log("Collision between " + gameObject.name + " and " + collision.gameObject.name);
    }
}
