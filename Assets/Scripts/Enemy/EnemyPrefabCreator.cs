using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyPrefabCreator
{
    public string enemy;
    public GameObject prefab;
    private EnemyConfig enemyConfig;
    private EnemyType enemyType;

    public void Create()
    {
        Enum.TryParse<EnemyType>(enemy, out enemyType);
        enemyConfig = new EnemyConfig(enemyType);
        prefab = new GameObject(enemy);
        prefab.tag = enemyConfig.tag;

        AddSpriteRenderer();
        AddAnimationController();
        AddRigidbody2D();
        AddCapsuleCollider2D();
        AddEnemyController();
        Save();

        UnityEngine.Object.DestroyImmediate(prefab);
    }

    private void AddAnimationController()
    {
        prefab.AddComponent<Animator>().runtimeAnimatorController = (
            RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath($"Assets/Artworks/Actors/Enemies/{enemy}/{enemy}Controller.controller",
            typeof(RuntimeAnimatorController)
        );
    }

    private void AddSpriteRenderer()
    {
        SpriteRenderer spriteRenderer = prefab.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = enemyConfig.sortingLayer;
        string[] guids = AssetDatabase.FindAssets($"{enemy.ToLower()}_idle_1 t:Sprite");
        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        Sprite sprite = (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
        spriteRenderer.sprite = sprite;
    }

    private void Save()
    {
        PrefabUtility.SaveAsPrefabAsset(prefab, $"Assets/Prefabs/{enemy}.prefab");
    }

    private void AddRigidbody2D()
    {
        Rigidbody2D rigidbody = prefab.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        rigidbody.freezeRotation = true;
    }

    private void AddCapsuleCollider2D()
    {
        CapsuleCollider2D capsuleCollider2D = prefab.AddComponent<CapsuleCollider2D>();
        capsuleCollider2D.size = enemyConfig.capsuleColliderSize;
        capsuleCollider2D.direction = enemyConfig.capsuleColliderDirection;
    }
    private void AddEnemyController()
    {
        System.Type controllerType = System.Type.GetType(enemy + "Controller");
        if (controllerType != null) {
            prefab.AddComponent(controllerType);
        } else {
            prefab.AddComponent<EnemyController>();
        }
    }
}
