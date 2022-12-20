using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldAmount = 5;
    protected override void OnCollect()
    {
        base.OnCollect();
        GetComponent<SpriteRenderer>().sprite = emptyChest;
        GameManager.instance.gold += goldAmount;
        GameManager.instance.SaveState();
    }
}
