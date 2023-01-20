using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : EnemyController
{
    public SkeletonController()
    {
        maxHealth = 20;
        colliderSizeX = 0.12f;
        colliderSizeY = 0.20f;
    }
    protected override void Start()
    {
        base.Start();
    }
}
