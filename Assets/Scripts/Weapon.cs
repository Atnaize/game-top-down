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

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
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

    protected override void OnCollide(Collider2D collider2D)
    {
        if (collider2D.name == "Player" || collider2D.tag != "Fighter") {
            return;
        }

        Damage damage = new Damage{
            damageAmount = damagePoint,
            origin = transform.position,
            pushForce = pushForce,
        };

        collider2D.SendMessage("ReceiveDamage", damage);
    }
    private void Attack()
    {
        animator.SetTrigger("WeaponTriggerSwing");
    }
}
