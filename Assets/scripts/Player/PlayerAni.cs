using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerAni : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, run, attack, dash, ulti, jump;
    public string currState;
    public string currentAnimation;

    private void Start()
    {
        currState = "idle";
        SetCharacterState(currState);
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        if (animation.name == currentAnimation && currentAnimation != "jump")
        {
            return;
        }
        currentAnimation = animation.name;
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timescale;
        animationEntry.Complete += AnimationEntry_Complete;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currState == "attack")
        {
            SetCharacterState("idle");
        }
        if (currState == "jump")
        {
            SetCharacterState("idle");
        }
        if (currState == "dash")
        {
            SetCharacterState("idle");
        }
        if (currState == "ulti")
        {
            SetCharacterState("idle");
        }
    }

    public void SetCharacterState(string state)
    {
        if (state == "attack")
        {
            SetAnimation(attack, false, 3f);
            currState = "attack";
        }
        else if (state == "run")
        {
            SetAnimation(run, true, 3.5f);
            currState = "run";
        }
        else if (state == "ulti")
        {
            SetAnimation(ulti, false, 2f);
            currState = "ulti";
        }
        else if (state == "dash")
        {
            SetAnimation(dash, false, 2f);
            currState = "dash";
        }
        else if (state == "jump")
        {
            SetAnimation(jump, false, 2f);
            currState = "jump";
        }
        else if (state == "idle")
        {
            SetAnimation(idle, true, 1f);
            currState = "idle";
        }
    }
}
