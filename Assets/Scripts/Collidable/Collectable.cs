using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool collected = false;

    protected override void OnCollide(Collider2D collider2D)
    {
        if (collected) {
            return;
        }

        if (collider2D.name == "Player") {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
