using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Skeleton,
    Slime,
}

public class EnemyConfig
{
    public Vector2 capsuleColliderSize = new Vector2(0.32f, 0.32f);
    public CapsuleDirection2D capsuleColliderDirection = CapsuleDirection2D.Vertical;
    public string tag = "Enemy";
    public string sortingLayer = "ForegroundCollide";

    public EnemyConfig(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Skeleton:
                capsuleColliderSize = new Vector2(0.12f, 0.20f);
                break;
            case EnemyType.Slime:
                capsuleColliderSize = new Vector2(0.15f, 0.15f);
                capsuleColliderDirection = CapsuleDirection2D.Horizontal;
                break;
            default:
                break;
        }
    }
}
