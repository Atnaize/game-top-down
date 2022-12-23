using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldAmount = 5;

    public override void Start()
    {
        base.Start();
        rb.isKinematic = true;
    }
    protected override void OnCollect()
    {
        base.OnCollect();

        GetComponent<SpriteRenderer>().sprite = emptyChest;
        GameManager.instance.gold += goldAmount;

        GameManager.instance.ShowFloatingText(
            "+" + goldAmount + " gold",
            25,
            Color.yellow,
            transform.position,
            Vector3.up * 25,
            1.5f
        );
    }
}
