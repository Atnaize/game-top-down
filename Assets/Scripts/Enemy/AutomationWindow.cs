using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class AutomationWindow : EditorWindow
{
    protected const string BASE_PATH = "Assets/Artworks/Actors/Enemies/";
    protected string enemy {get; set;}

    [MenuItem("Window/Automation Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AutomationWindow));
    }

    private void OnGUI()
    {
        foreach (DirectoryInfo directory in this.GetFolders())
        {
            enemy = directory.Name;
            this.CreateHorizontalBox();
        }
    }

    private DirectoryInfo[] GetFolders()
    {
        return new DirectoryInfo(BASE_PATH).GetDirectories("*.*");
    }

    private void CreateHorizontalBox()
    {
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        EditorGUILayout.LabelField(this.enemy, EditorStyles.boldLabel);

        CreateAnimationButton();
        CreatePrefabButton();

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    private void CreateAnimationButton()
    {
        Type enemyType = Type.GetType(enemy + "AnimationCreator");
        EditorGUI.BeginDisabledGroup(enemyType == null);

        if (GUILayout.Button("Create animations"))
        {
            EnemyAnimationCreator enemyAnimationCreator = ScriptableObject.CreateInstance(enemyType) as EnemyAnimationCreator;
            enemyAnimationCreator.CreateAnimations(enemy);
        }

        EditorGUI.EndDisabledGroup();
    }

    private void CreatePrefabButton()
    {
        if (GUILayout.Button("Create prefab"))
        {
            EnemyPrefabCreator prefabCreator = new EnemyPrefabCreator();
            prefabCreator.enemy = enemy;
            prefabCreator.Create();
        }
    }
}