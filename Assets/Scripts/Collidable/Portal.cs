using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : Collidable
{
    public string[] scenesNames = {"Dungeon1"};

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        string scenesName = scenesNames[Random.Range(0, scenesNames.Length)];
        SceneManager.LoadScene(scenesName);
    }
}
