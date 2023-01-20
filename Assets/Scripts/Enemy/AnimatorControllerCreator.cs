using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

/**
 * To generate differents animations for all 4 directions
 * Refac CreateBlendTree in order to pass 4 differents animations
 * Currently only 1 is passed to we have the same animation for top/down/left/right move
 * Left/Right is managed by fliping the sprite on the dedicated class
 */
public class AnimatorControllerCreator
{    public string enemy;

    private AnimatorController animatorController;

    private const string DEFAULT_PATH = "Assets/Artworks/Actors/Enemies/";
    private const string PARAMETER_X = "X";
    private const string PARAMETER_Y = "Y";
    private const string PARAMETER_MOVING = "isMoving";
    private const string PARAMETER_ATTACKING = "isAttacking";

    public void Start()
    {
        // Create the Animator Controller asset at the specified path
        animatorController = AnimatorController.CreateAnimatorControllerAtPath($"{DEFAULT_PATH}{enemy}/{enemy}Controller.controller");

        AddParametersToAnimatorController();
        AddTransitionsBetweenMotions();

        // Save the Animator Controller asset
        AssetDatabase.SaveAssets();
    }

    private void AddTransitionsBetweenMotions()
    {
        AnimatorState idleState = CreateState("Idle", (AnimationClip)AssetDatabase.LoadAssetAtPath($"{DEFAULT_PATH}{enemy}/{enemy}Idle.anim", typeof(AnimationClip)));
        AnimatorState move = CreateState("Move", (AnimationClip)AssetDatabase.LoadAssetAtPath($"{DEFAULT_PATH}{enemy}/{enemy}Move.anim", typeof(AnimationClip)));
        AnimatorState attackState = CreateState("Attack", (AnimationClip)AssetDatabase.LoadAssetAtPath($"{DEFAULT_PATH}{enemy}/{enemy}Attack.anim", typeof(AnimationClip)));

        CreateTransition(idleState, move, AnimatorConditionMode.If, PARAMETER_MOVING);
        CreateTransition(idleState, attackState, AnimatorConditionMode.If, PARAMETER_ATTACKING);

        CreateTransition(move, idleState, AnimatorConditionMode.IfNot, PARAMETER_MOVING);
        CreateTransition(move, attackState, AnimatorConditionMode.If, PARAMETER_ATTACKING);

        CreateTransition(attackState, idleState, AnimatorConditionMode.IfNot, PARAMETER_MOVING, 1f);
        CreateTransition(attackState, move, AnimatorConditionMode.If, PARAMETER_MOVING, 1f);
    }

    private AnimatorState CreateState(string name, AnimationClip clip)
    {
        AnimatorState state = animatorController.layers[0].stateMachine.AddState(name);
        state.motion = CreateBlendTree(clip);

        return state;
    }

    private AnimatorStateTransition CreateTransition(AnimatorState from, AnimatorState to, AnimatorConditionMode condition, string parameter, float exitTime = 0f)
    {
        AnimatorStateTransition transition = from.AddTransition(to);
        transition.AddCondition(condition, 1, parameter);
        transition.hasFixedDuration = true;
        transition.duration = 0;

        if (exitTime != 0) {
            transition.hasExitTime = true;
            transition.exitTime = exitTime;
        }

        return transition;
    }
    private void AddParametersToAnimatorController()
    {
        animatorController.AddParameter(CreateAnimatorControllerParameter(PARAMETER_X, AnimatorControllerParameterType.Float));
        animatorController.AddParameter(CreateAnimatorControllerParameter(PARAMETER_Y, AnimatorControllerParameterType.Float));
        animatorController.AddParameter(CreateAnimatorControllerParameter(PARAMETER_MOVING, AnimatorControllerParameterType.Bool));
        animatorController.AddParameter(CreateAnimatorControllerParameter(PARAMETER_ATTACKING, AnimatorControllerParameterType.Trigger));
    }

    private AnimatorControllerParameter CreateAnimatorControllerParameter(string name, AnimatorControllerParameterType type)
    {
        AnimatorControllerParameter animatorControllerParameter = new AnimatorControllerParameter();

        animatorControllerParameter.name = name;
        animatorControllerParameter.type = type;

        return animatorControllerParameter;
    }

    private BlendTree CreateBlendTree(AnimationClip animationClip)
    {
        BlendTree blendTree = new BlendTree();

        blendTree.name = animationClip.name;
        blendTree.blendType = BlendTreeType.SimpleDirectional2D;
        blendTree.blendParameter = PARAMETER_X;
        blendTree.blendParameterY = PARAMETER_Y;

        blendTree.AddChild(animationClip, new Vector2(0, -1));
        blendTree.AddChild(animationClip, new Vector2(1, 0));
        blendTree.AddChild(animationClip, new Vector2(0, 1));
        blendTree.AddChild(animationClip, new Vector2(-1, 0));

        return blendTree;
    }
}
