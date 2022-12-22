using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 3;

    protected override void OnCollide(Collider2D collider2D)
    {
        if (collider2D.tag == "Fighter" && collider2D.name == "Player")
        {
            Damage damage = new Damage{
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce,
            };

            collider2D.SendMessage("ReceiveDamage", damage);
        }
    }
}
