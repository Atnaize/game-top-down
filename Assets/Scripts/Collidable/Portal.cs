using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : Collidable
{
    public string[] scenesNames = {"Dungeon1"};

    protected override void OnCollide(Collider2D collider2D)
    {
        string scenesName = scenesNames[Random.Range(0, scenesNames.Length)];
        SceneManager.LoadScene(scenesName);
    }
}
