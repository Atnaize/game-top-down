using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class EnemyAnimationCreator: AutomationWindow
{
    protected List<Animation> animations;

    public void CreateAnimations(string enemy)
    {
        this.enemy = enemy;

        foreach (Animation animation in GetAnimations())
        {
            CreateAnimation(animation.name, animation.frameRate);
        }

        AnimatorControllerCreator animatorControllerCreator = new AnimatorControllerCreator();
        animatorControllerCreator.enemy = this.enemy;
        animatorControllerCreator.Start();
    }

    protected abstract List<Animation> GetAnimations();

    private void CreateAnimation(string state, int frameRate)
    {
        AnimationCreator animationCreator = new AnimationCreator();

        UnityEngine.Object[] slices = AssetDatabase.LoadAllAssetsAtPath($"Assets/Artworks/Actors/Enemies/{enemy}/{enemy.ToLower()}.png");
        List<Sprite> sprites = new List<Sprite>();

        for (int i = 0; i < slices.Length; i++)
        {
            if (slices[i].name.StartsWith($"{enemy.ToLower()}_{state.ToLower()}"))
            {
                sprites.Add((Sprite)slices[i]);
            }
        }

        Debug.Log(sprites.Count);

        animationCreator.sprites = sprites.ToArray();
        animationCreator.enemy = enemy;
        animationCreator.frameRate = frameRate;
        animationCreator.state = state;

        animationCreator.Create();
    }
}

public struct Animation
{
    public string name;
    public int frameRate;
}