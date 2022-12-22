using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int experience = 1;
    public float triggerLenght = 1.0f;
    public float chaseLenght = 2.0f;
    public ContactFilter2D filter;
    private bool chasing = false;
    private bool collidingWithPlayer = false;
    private Transform playerTransform;
    private Vector3 startingPosition;

    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();

        playerTransform = GameObject.Find("Player").transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        MoveWhenPlayerApproach();

        collidingWithPlayer = false;

        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (!hits[i]) {
                continue;
            }

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }

    }

    private void MoveWhenPlayerApproach()
    {
        float distanceWithPlayer = Vector3.Distance(playerTransform.position, startingPosition);

        if (distanceWithPlayer >= chaseLenght)
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
            return;
        }

        if (distanceWithPlayer < triggerLenght)
            chasing = true;

        if (chasing)
        {
            if (!collidingWithPlayer)
            {
                UpdateMotor((playerTransform.position - transform.position).normalized);
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }

    }

    protected override void Die()
    {
        Destroy();
        GrantXP();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void GrantXP()
    {
        GameManager.instance.xp += experience;
        DisplayXP();
    }

    private void DisplayXP()
    {
        GameManager.instance.ShowFloatingText(
            "+ " + experience + "xp",
            25,
            Color.white,
            playerTransform.position,
            Vector3.up * 40,
            1.0f
        );
    }
}
