using UnityEditor;
using UnityEngine;

public class AnimationCreator
{
    public Sprite[] sprites;
    public string enemy;
    public string state;
    public int frameRate = 10;

    private const string DEFAULT_PATH = "Assets/Artworks/Actors/Enemies/";

    public void Create()
    {
        AnimationClip idleClip = new AnimationClip();
        idleClip.frameRate = frameRate;

        AnimationClipSettings clipSettings = new AnimationClipSettings();
        clipSettings.loopTime = true;
        AnimationUtility.SetAnimationClipSettings(idleClip, clipSettings);

        ObjectReferenceKeyframe[] spriteKeyframes = new ObjectReferenceKeyframe[sprites.Length];

        for (int i = 0; i < sprites.Length; i++)
        {
            spriteKeyframes[i] = new ObjectReferenceKeyframe();
            spriteKeyframes[i].time = (float)i / 10;
            spriteKeyframes[i].value = sprites[i];
        }

        EditorCurveBinding spriteBinding = new EditorCurveBinding();
        spriteBinding.type = typeof(SpriteRenderer);
        spriteBinding.path = "";
        spriteBinding.propertyName = "m_Sprite";

        AnimationUtility.SetObjectReferenceCurve(idleClip, spriteBinding, spriteKeyframes);

        AssetDatabase.CreateAsset(idleClip, $"{DEFAULT_PATH}{enemy}/{enemy}{state}.anim");
    }

}
