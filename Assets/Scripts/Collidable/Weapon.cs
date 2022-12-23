using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public int damagePoint = 1;
    public float pushForce = 3.0f;

    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float cooldown = 0.5f;
    private float lastAttack;

    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.isKinematic = true;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastAttack > cooldown)
            {
                lastAttack = Time.time;
                Attack();
            }
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.tag != "Fighter") {
            return;
        }

        Damage damage = new Damage{
            damageAmount = damagePoint,
            origin = transform.position,
            pushForce = pushForce,
        };

        collision.gameObject.SendMessage("ReceiveDamage", damage);
    }
    private void Attack()
    {
        animator.SetTrigger("WeaponTriggerSwing");
    }
}
