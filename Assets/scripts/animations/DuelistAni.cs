using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class DuelistAni : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset run, attack;
    public string currState;

    private void Start()
    {
        currState = "run";
        SetCharacterState(currState);
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timescale)
    {
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timescale;
        animationEntry.Complete += AnimationEntry_Complete;
    }

    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currState == "attack")
        {
            SetCharacterState("run");
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
            SetAnimation(run, true, 3f);
            currState = "run";
        }
    }


}
