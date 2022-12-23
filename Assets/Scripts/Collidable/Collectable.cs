using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool collected = false;

    public override void Start()
    {
        base.Start();
        rb.isKinematic = true;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collected) {
            return;
        }

        if (collision.gameObject.name == "Player") {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
